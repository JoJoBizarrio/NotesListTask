using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotesListTask.Model
{
    public class Note : INotifyPropertyChanged
    {
        private string _title;
        private string _description;

        public int Id { get; set; }

        public string Title
        {
            get 
            { 
                return _title; 
            }

            set 
            {
                _title = value; 
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get { return _description; } 

            set
            {
                _description= value; 
                OnPropertyChanged(nameof(Description));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
