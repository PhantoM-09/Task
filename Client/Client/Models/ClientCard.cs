using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Client.Models
{
    public class ClientCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BitmapImage Image { get; set; }

        public ClientCard()
        {

        }

        public ClientCard(int id, string name, BitmapImage image)
        {
            this.Id = id;
            this.Name = name;
            this.Image = image;
        }
    }
}
