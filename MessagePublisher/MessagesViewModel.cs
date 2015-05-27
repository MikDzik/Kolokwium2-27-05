using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MessagePublisher
{
    /// <summary>
    /// Your TODO: please follow insstruction 
    /// </summary>
    public class MessagesViewModel : IMessagesViewModel
    {
        private readonly IDispatcher _dispatcher;

        public MessagesViewModel(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;            
        }


        public string UserName
        {
            get { return ObservedUsers.First<UserQueue>().Owner ; }
        }

        public UserQueue SelectedUser
        {
            get
            {
                return SelectedUser;
            }
            set
            {
                SelectedUser = ObservedUsers.First<UserQueue>();
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SelectedUser"));

            }
        }

        public IEnumerable<UserQueue> ObservedUsers
        {
            get { return Globals.GetRandomDataForAllUsers(); }
        }

        public string NewMessageText
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Windows.Input.ICommand PublishCommand
        {
            get
            {
                Message message = new Message();
                message.Author = UserName;
                message.Content = NewMessageText;
                message.PublishedOn = DateTime.Now;
                SelectedUser.Messages.Add(message);
            }
        }

        public DateTime FromDate
        {
            get
            {
                return FromDate;
            }
            set
            {
                FromDate = DateTime.Now.AddYears(-1);
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("FromDate"));
            }
        }

        public DateTime ToDate
        {
            get
            {
                return ToDate;
            }
            set
            {
                ToDate = DateTime.Now;
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ToDate"));
            }
        }

        public string TextFilter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Windows.Input.ICommand FilterCommand
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Message> FilteredMessages
        {
            get { FilterCommandMethod(); }
        }

        public System.Windows.Input.ICommand SaveCommand
        {
            get
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Message>));

                using (FileStream stream = File.OpenWrite("myXml"))
                {
                    List<Message> list = SelectedUser.Messages;
                    serializer.Serialize(stream, list);
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;


        System.Windows.Input.List<Message> FilterCommandMethod()
        {
            var list = new List<MessageSearcher>();

            if ((TextFilter != null) &&
                (TextFilter != "") &&
                (SelectedUser != null) &&
                (SelectedUser.Messages != null))
            {
                list.Add(new DateMessageSearcher(FromDate, ToDate));
                list.Add(new TextMessageSearcher(TextFilter));
                return list;
            }
            else return list = null;
        }
    }
}
