using Newtonsoft.Json;
using Structure.Model;
using Structure.Utils;
using Structure.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.ViewModel
{
  public  class MessagingPageViewModel:INotifyPropertyChanged
    {
        ActivityIndicator activitityIndicator = null;
        public MessagingPageViewModel(ActivityIndicator actIndicator)
        {
            // Task.Run(async()=> this.messages=await getListCommunication()) ;



            this.activitityIndicator = actIndicator;
        }

        public async Task loadCommunication()
        {
            activitityIndicator.IsVisible = true;
            activitityIndicator.IsRunning = true;
            this.Messages = await getListCommunication();
            activitityIndicator.IsVisible = false;
            activitityIndicator.IsRunning = false;
        }

        public ObservableCollection<Communication> Messages
        {
            get { return messages; }
            set
            {
                if(messages !=value)
                {
                    messages = value;
                    OnPropertyChanged();
                }
            }
        }
          
        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
                OnPropertyChanged();

              //  sendMessageCommand.ChangeCanExecute();
            }
        }
        //public bool IsConnected=>clien
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // readonly WebSocket.Portable.WebSocketClient client;
        //readonly CancellationTokenSource cts;

        Command<string> sendMessageCommand => new Command<string>(async(msg)=> await SendMessageAsync(msg),CanSendMessage);
        public Command<string> SendMessage => sendMessageCommand;
        ObservableCollection<Communication> messages;
        string messageText;
        readonly string username;
        readonly string requestUri =string.Concat(Constants.GetCommunicationrequestUri, "6FDFDD074B4BD30209085207575E5D0D");
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential { UserName = Constants.ApiUserName, Password = Constants.ApiPassword };
        
        private async Task<ObservableCollection<Communication>> getListCommunication()
        {
            try {
                var handler = new HttpClientHandler { Credentials = credentials };
                var client = new HttpClient(handler);
                var json = await client.GetStringAsync(requestUri);
                var communicationLists = JsonConvert.DeserializeObject<ObservableCollection<Communication>>(json);
                return communicationLists;
            }
            catch
            {
               await new Page().DisplayAlert("Network Error", "No Connection Internet", "OK");
                return default(ObservableCollection<Communication>);
            }
            
        }

        //TO-DO: Wait POST Message API
        private async Task SendMessageAsync(string message)
        {
            try
            {
                var msg = new 
                {
                    ClientKey = "6FDFDD074B4BD30209085207575E5D0D",
                    comments = message,
                   
                };
               
                var postUri = Constants.PostCommunication;
                var handler = new HttpClientHandler { Credentials = credentials };
                var client = new HttpClient(handler);
                var json = JsonConvert.SerializeObject(msg);
                var data =new StringContent( JsonConvert.SerializeObject(msg),Encoding.UTF8,"application/json");
               
               
               
                   MessageText = string.Empty;
                   this.Messages.Add(new Communication {Auteur="You",Text=msg.comments,Date=DateTime.Now,Position="R" });

                await client.PostAsync(postUri, data);


            }
            catch(Exception e)
            {
               
               
            }
           
           
        }
        private bool CanSendMessage(string message)
        {
            return !string.IsNullOrEmpty(message); 
        }
        
    }
}
