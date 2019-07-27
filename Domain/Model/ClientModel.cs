using System;

namespace Domain.Model
{
    public class ClientModel
    {
        public ClientModel(string[] array)
        {
            Id = Convert.ToInt32(array[0]);
            Cnpj = array[1];
            Name = array[2];
            BusinessArea = array[3];
        }

        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string BusinessArea { get; set; }
    }
}
