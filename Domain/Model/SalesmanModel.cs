using System;

namespace Domain.Model
{
    public class SalesmanModel
    {
        public SalesmanModel(string[] array)
        {
            Id = Convert.ToInt32(array[0]);
            Cpf = array[1];
            Name = array[2];
            Salary = Convert.ToDecimal(array[3]);
        }

        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
}
