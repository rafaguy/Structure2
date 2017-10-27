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
        #region Private field
         ObservableCollection<Communication> messages;
        string messageText;
        static readonly string requestUri = string.Concat(Constants.GetCommunicationrequestUri, "6FDFDD074B4BD30209085207575E5D0D");
        static System.Net.NetworkCredential credentials = new System.Net.NetworkCredential { UserName = Constants.ApiUserName, Password = Constants.ApiPassword };
        #endregion
        Command<string> sendMessageCommand => new Command<string>(async (msg) => await SendMessageAsync(msg), CanSendMessage);
        public Command<string> SendMessage => sendMessageCommand;

        public MessagingPageViewModel()
        {

            
            this.Messages = GlobalCommunicationDataSource.Messages;
        }

        public  async Task loadCommunication()
        {
         
           this. Messages = await getListCommunication();
           
        }

        public  ObservableCollection<Communication> Messages
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
        public  event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

       
       
        
        public static async Task<ObservableCollection<Communication>> getListCommunication()
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
                   Messages.Add(new Communication {Auteur="You",Text=msg.comments,Date=DateTime.Now,Position="R" });

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
