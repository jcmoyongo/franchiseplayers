using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FP.Models
{
    public class Team: INotifyPropertyChanged
    {
        private string _code;
        private string _fullName;
        private string _shortName;
        private string _nickName;
        private string __division;
        private string _conference;
        private string _imageFileName;
        private string _arena;
        private bool? _isSelected;

        public string Code { get => _code; set => _code = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string ShortName { get => _shortName; set => _shortName = value; }
        public string NickName { get => _nickName; set => _nickName = value; }
        public string Division { get => __division; set => __division = value; }
        public string Conference { get => _conference; set => _conference = value; }
        public string Arena { get => _arena; set => _arena = value; }
        public string ImageFileName {get => _imageFileName; set => _imageFileName = value;}
        public bool? IsSelected 
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }


        public Team(string code, string fullName, string shortname, string nickName, string division, string conference, string arena)
        {
            Code = code;
            FullName = fullName;
            ShortName = shortname;
            NickName = nickName;
            Division = division;
            Conference = conference;
            Code = code;
            FullName = fullName;
            ShortName = shortname;
            NickName = nickName;
            Division = division;
            Conference = conference;
            Arena = arena;
            ImageFileName = "blank.png";//$"{nickName}.png";
        }

        public override string ToString()
        {
            return this.Code;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
