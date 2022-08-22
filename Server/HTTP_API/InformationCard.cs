using System;

namespace HTTP_API
{
    [Serializable]
    public class InformationCard
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        public InformationCard()
        {

        }

        public InformationCard(int id, string name, byte[] image)
        {
            this._id = id;
            this._name = name;
            this._image = image;
        }
    }
}
