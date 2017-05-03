using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sfs2X.Core;
using Sfs2X;
using Sfs2X.Util;
using Sfs2X.Entities.Data;
using Sfs2X.Logging;
using Sfs2X.Entities;
using Sfs2X.Requests;

namespace SmartFox2X_Client_Chat
{
    public partial class Form1 : Form
    {
        private SmartFox smartFox;

        private string defaultHost = "192.168.122.97";   // Default host
        private int defaultTcpPort = 9933;          // Default TCP port
        private int defaultWsPort = 8888;           // Default WebSocket port
        private string zoneName = string.Empty;
        private string roomName = string.Empty;

        private const string GAMENAME = "candyland";
        private const string KEY_GN = "gameName";
        private const string CMD_GP_GAME = "cmd.game";
        private const string CMD_GP_OTHER = "cmd.other";

        private object lockOfExtensionQueue = new object();
        private Queue<string> receiveExtensionQueue = new Queue<string>();


        private string chatString;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            Disconnect();
            button_ServerStart.Enabled = true;
            button_Send.Enabled = false;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            ////
            //if(textBox_UserMsg.Text.Length > 0)
            //{
            //    smartFox.Send(new Sfs2X.Requests.PublicMessageRequest(textBox_UserMsg.Text));
            //}

            ISFSObject mISFSObject = new SFSObject();
            mISFSObject.PutText("UserMsg", textBox_UserMsg.Text);
            smartFox.Send(new ExtensionRequest("UserMsg", mISFSObject));

            textBox_UserMsg.Text = "";
        }

        private void button_ServerStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            button_ServerStart.Enabled = false;
            button_Login.Enabled = true;
            button_Send.Enabled = true;          

 
            defaultHost = textBox_ServerIP.Text;
            defaultTcpPort = int.Parse(textBox_ServerPort.Text);
            zoneName = textBox_ZoneName.Text;
            roomName = textBox_RoomName.Text;

            if (smartFox == null || !smartFox.IsConnected)
            {
                // CONNECT
                // Initialize SFS2X client and add listeners
                smartFox = new SmartFox();

                // Set ThreadSafeMode explicitly, or Windows Store builds will get a wrong default value (false)
                smartFox.ThreadSafeMode = true;
                //Connect
                smartFox.AddEventListener(SFSEvent.CONNECTION, OnConnection);
                smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
                //System Message
                smartFox.AddLogListener(LogLevel.INFO, OnInfoMessage);
                smartFox.AddLogListener(LogLevel.WARN, OnWarnMessage);
                smartFox.AddLogListener(LogLevel.ERROR, OnErrorMessage);
                //Lobby
                smartFox.AddEventListener(SFSEvent.LOGIN, OnLogin);
                smartFox.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
                smartFox.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdd);
                smartFox.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
                smartFox.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
                smartFox.AddEventListener(SFSEvent.ROOM_VARIABLES_UPDATE, OnRoomVariablesUpate);
                smartFox.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
                smartFox.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
                smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
                smartFox.AddEventListener(SFSEvent.USER_COUNT_CHANGE, OnUserCountChange);
                smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);

                // Set connection parameters
                ConfigData cfg = new ConfigData();
                cfg.Host = defaultHost;
                cfg.Port = Convert.ToInt32(defaultTcpPort);
                cfg.Zone = zoneName;
                cfg.Debug = true;

                // Connect to SFS2X
                Console.WriteLine("Connect");
                smartFox.Connect(cfg);
            }
            else
            {
                // DISCONNECT

                // Disconnect from SFS2X
                Disconnect();
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");

            // Remove SFS2X listeners
            //Connect
            smartFox.RemoveEventListener(SFSEvent.CONNECTION, OnConnection);
            smartFox.RemoveEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            //System Message
            smartFox.RemoveLogListener(LogLevel.INFO, OnInfoMessage);
            smartFox.RemoveLogListener(LogLevel.WARN, OnWarnMessage);
            smartFox.RemoveLogListener(LogLevel.ERROR, OnErrorMessage);
            //Lobby
            smartFox.RemoveEventListener(SFSEvent.LOGIN, OnLogin);
            smartFox.RemoveEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            smartFox.RemoveEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
            smartFox.RemoveEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
            smartFox.RemoveEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
            smartFox.RemoveEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
            smartFox.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);

            smartFox.Disconnect();

            smartFox = null;
        }

        //----------------------------------------------------------
        // SmartFoxServer event listeners
        //----------------------------------------------------------
        private void OnConnection(BaseEvent evt)
        {
            Console.WriteLine("OnConnect");

            if ((bool)evt.Params["success"])
            {
                Console.WriteLine("Connection established successfully");
                Console.WriteLine("SFS2X API version: " + smartFox.Version);
                Console.WriteLine("Connection mode is: " + smartFox.ConnectionMode);

                smartFox.Send(new LoginRequest(""));
            }
            else
            {
                Console.WriteLine("Connection failed; is the server running at all?");

                // Remove SFS2X listeners and re-enable interface
                Disconnect();
            }
        }

        private void OnConnectionLost(BaseEvent evt)
        {
            Console.WriteLine("Connection was lost; reason is: " + (string)evt.Params["reason"]);

            // Remove SFS2X listeners and re-enable interface
            Disconnect();
        }

        //----------------------------------------------------------
        // SmartFoxServer log event listeners
        //----------------------------------------------------------

        public void OnInfoMessage(BaseEvent evt)
        {
            Console.WriteLine("OnInfoMessage");

            string message = (string)evt.Params["message"];
            ShowLogMessage("INFO", message);
        }

        public void OnWarnMessage(BaseEvent evt)
        {
            Console.WriteLine("OnWarnMessage");

            string message = (string)evt.Params["message"];
            ShowLogMessage("WARN", message);
        }

        public void OnErrorMessage(BaseEvent evt)
        {
            Console.WriteLine("OnErrorMessage");

            string message = (string)evt.Params["message"];
            ShowLogMessage("ERROR", message);
        }

        private void ShowLogMessage(string level, string message)
        {
            Console.WriteLine("ShowLogMessage");

            message = "[SFS > " + level + "] " + message;
            Console.WriteLine(message);
            Console.WriteLine(message);
        }

        #region Lobby
        private void OnLogin(BaseEvent evt)
        {
            Console.WriteLine("OnLogin");

            User user = (User)evt.Params["user"];

            // Show system message
            string msg = "Connection established successfully\n";
            msg += "SFS2X API version: " + smartFox.Version + "\n";
            msg += "Connection mode is: " + smartFox.ConnectionMode + "\n";
            msg += "Logged in as " + user.Name;
            Console.WriteLine("System Message: " + msg);

            // Populate Room list
            // For the roomlist we use a scrollable area containing a separate prefab button for each Room
            // Buttons are clickable to join Rooms
            string roomListString = string.Empty;
            foreach (Room room in smartFox.RoomList)
            {
                int roomId = room.Id;
                roomListString += room.Id.ToString() + ", ";
            }

            Console.WriteLine("roomList: " + roomListString);


            // Join first Room in Zone
            if (smartFox.RoomList.Count > 0)
            {
                smartFox.Send(new Sfs2X.Requests.JoinRoomRequest(roomName));
            }
        }

        private void OnLoginError(BaseEvent evt)
        {
            Console.WriteLine("OnLoginError");

            // Disconnect
            smartFox.Disconnect();

            // Remove SFS2X listeners and re-enable interface
            Disconnect();

            // Show error message
            Console.WriteLine("Login failed: " + (string)evt.Params["errorMessage"]);
        }

        private void OnRoomAdd(BaseEvent evt)
        {
            Console.WriteLine("OnRoomAdd");
        }

        private void OnRoomJoin(BaseEvent evt)
        {
            Console.WriteLine("OnRoomJoin");

            Room room = (Room)evt.Params["room"];

            // Show system message
            Console.WriteLine("\nYou joined room '" + room.Name + "'\n");

            // Populate users list
            // For the userlist we use a simple text area, with a user name in each row
            // No interaction is possible in this example

            // Get user names
            List<string> userNames = new List<string>();
            string userNameListString = string.Empty;

            foreach (User user in room.UserList)
            {
                if (user != smartFox.MySelf)
                {
                    userNames.Add(user.Name);
                    userNameListString += user.Name + ", ";
                }
            }

            //Move to CandylandExtraBetController
            //Start
            //Debug.Log("User Name List: " + userNameListString);

            //SFSObject mSFSObject = SFSObject.NewInstance();

            //string data = string.Format("{\"type\":\"REQ_CA\",\"timestamp\":%d,\"status\":\"NONE\",\"typePL\":\"REQ_INIT\",\"payload\":\"%s\",\"isCompressed\":\"false\"}", DateTime.Now.Ticks, "{\\\"type\\\":\\\"REQ_INIT\\\",\\\"gameName\\\":\\\"candyland\\\"}");

            //mSFSObject.PutText("data", data);

            //smartFox.Send(new Sfs2X.Requests.ExtensionRequest("cmd.game", mSFSObject));
            //End
        }

        private void OnRoomJoinError(BaseEvent evt)
        {
            Console.WriteLine("OnRoomJoinError");

            // Show error message
            Console.WriteLine("Room join failed: " + (string)evt.Params["errorMessage"]);
        }

        private void OnRoomVariablesUpate(BaseEvent evt)
        {
            Console.WriteLine("OnRoomVariablesUpate");
        }

        private void OnPublicMessage(BaseEvent evt)
        {
            Console.WriteLine("OnPublicMessage");

            User sender = (User)evt.Params["sender"];
            string message = (string)evt.Params["message"];

            Console.WriteLine("PublicMessage: " + sender + ", " + message);
        }

        private void OnUserEnterRoom(BaseEvent evt)
        {
            Console.WriteLine("OnUserEnterRoom");

            User user = (User)evt.Params["user"];
            Room room = (Room)evt.Params["room"];

            // Show system message
            Console.WriteLine("User " + user.Name + " entered the room");

            // Populate users list
            string roomListString = string.Empty;
            foreach (Room room2 in smartFox.RoomList)
            {
                int roomId = room2.Id;
                roomListString += room2.Id.ToString() + ", ";
            }

            Console.WriteLine("roomList: " + roomListString);
        }

        private void OnUserExitRoom(BaseEvent evt)
        {
            Console.WriteLine("OnUserExitRoom");
            
            User user = (User)evt.Params["user"];

            if (user != smartFox.MySelf)
            {
                Room room = (Room)evt.Params["room"];

                // Show system message
                Console.WriteLine("User " + user.Name + " left the room");

                // Populate users list
                // For the userlist we use a simple text area, with a user name in each row
                // No interaction is possible in this example

                // Get user names
                List<string> userNames = new List<string>();
                string userNameListString = string.Empty;

                foreach (User user2 in room.UserList)
                {
                    if (user2 != smartFox.MySelf)
                    {
                        userNames.Add(user2.Name);
                        userNameListString += user2.Name + ", ";
                    }
                }

                Console.WriteLine("User Name List: " + userNameListString);
            }
        }

        private void OnUserCountChange(BaseEvent evt)
        {
            Console.WriteLine("OnUserCountChange");
        }

        private void OnExtensionResponse(BaseEvent evt)
        {
            lock (lockOfExtensionQueue)
            {
                Console.WriteLine("OnExtensionResponse");

                string cmd = (string)evt.Params["cmd"];
                ISFSObject mISFSObject = (SFSObject)evt.Params["params"];

                Console.WriteLine("Current cmd:" + cmd);

                switch(cmd)
                {
                    default:
                        {

                        }
                        break;

                    case CMD_GP_GAME:
                        {
                            Console.WriteLine("RespMsg: " + mISFSObject.GetText("data"));
                        }
                        break;

                    case CMD_GP_OTHER:
                        {
                            Console.WriteLine("RespMsg: other test");
                        }
                        break;

                    case "UserMsg":
                        {
                            //MessageBox.Show("RespMsg: " + mISFSObject.GetText("UserMsg"));
                            //chatString += mISFSObject.GetText("UserMsg") + "\n";
                            chatString = mISFSObject.GetText("UserMsg");

                            //if(chatString.Contains("SS"))
                            //{
                            //    richTextBox_Log.Text += "Match S\n";
                            //}
                            //else
                            //{
                            //    richTextBox_Log.Text += "Not Match SS\n";
                            //}


                            richTextBox_Char.Text = chatString;

                            richTextBox_Char.SelectionStart = richTextBox_Char.Text.Length;
                            richTextBox_Char.ScrollToCaret();
                        }
                        break;
                }

                receiveExtensionQueue.Enqueue(mISFSObject.GetText("data"));
            }
        }

        public long GetExtensionPacketQueueLength()
        {
            return receiveExtensionQueue.Count;
        }

        public string GetExtensionPacket()
        {
            lock (lockOfExtensionQueue)
            {
                if (receiveExtensionQueue.Count > 0)
                {
                    return receiveExtensionQueue.Dequeue();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void SendExtensionPacket(string jsonString)
        {
            SFSObject mSFSObject = SFSObject.NewInstance();

            //string data = string.Format("{\"type\":\"REQ_CA\",\"timestamp\":%d,\"status\":\"NONE\",\"typePL\":\"REQ_INIT\",\"payload\":\"%s\",\"isCompressed\":\"false\"}", DateTime.Now.Ticks, "{\\\"type\\\":\\\"REQ_INIT\\\",\\\"gameName\\\":\\\"candyland\\\"}");

            mSFSObject.PutText("data", jsonString);

            smartFox.Send(new Sfs2X.Requests.ExtensionRequest("cmd.game", mSFSObject));
        }
        #endregion

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (smartFox != null)
            {
                smartFox.ProcessEvents();
            }
        }
    }
}
