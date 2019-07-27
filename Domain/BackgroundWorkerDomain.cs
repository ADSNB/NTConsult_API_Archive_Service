using Domain.Model;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain
{
    public class BackgroundWorkerDomain : IBackgroundWorkerDomain
    {
        #region private properties

        private List<ClientModel> _clientList;
        private List<SalesmanModel> _salesmanList;
        private List<SaleModel> _saleList;
        private readonly IConfiguration _configuration;

        #endregion

        public BackgroundWorkerDomain(IConfiguration configuration) => _configuration = configuration;

        public void ReadFile(List<ClientModel> clientList, List<SalesmanModel> salesmanList, List<SaleModel> saleList)
        {
            try
            {
                Log.Information("Read file initiated");

                _clientList = clientList;
                _salesmanList = salesmanList;
                _saleList = saleList;
                var dirFiles = Directory.GetFiles(GetDirectoryPath("In"), "*.dat");
                foreach (var dirFile in dirFiles)
                {
                    foreach (string line in File.ReadLines(dirFile, Encoding.UTF8))
                        ProccessLine(line);

                    CreateProcessedFile(dirFile);

                    MoveProcessedFile(dirFile);

                }

                Log.Information("Read file terminated");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region private methods
        //private const string HasCpf = @"(^\d{3}\.\d{3}\.\d{3}-\d{2})|(^\d{3}\d{3}\d{3}\d{2})$";
        //private const string HasCnpj = @"^(\d{3})\.?(\d{3})\.?(\d{3})\-?(\d{2}$)$|^(\d{2})\.?(\d{3})\.?(\d{3})\/?([0-1]{4})\-?(\d{2})$";
        private static string HomepathDirectory = Environment.GetEnvironmentVariable("HOMEPATH");

        private string GetDirectoryPath(string appSetting) => $"{HomepathDirectory}{_configuration.GetSection("Directory")[appSetting]}";

        private void ProccessLine(string line)
        {
            var array = line.Split('ç');

            switch (array[0])
            {
                case "001":
                    _salesmanList.Add(new SalesmanModel(array));
                    break;
                case "002":
                    _clientList.Add(new ClientModel(array));
                    break;
                case "003":
                    _saleList.Add(new SaleModel(array));
                    break;
            }
        }

        private void CreateProcessedFile(string dirFile)
        {
            if (!Directory.Exists(GetDirectoryPath("Out")))
                Directory.CreateDirectory(GetDirectoryPath("Out"));

            using (var fs = File.Create(GenerateFileAlias("Out", Path.GetFileName(dirFile))))
            {
                AddText(fs, $"Quantidade de clientes: {_clientList.Count}");
                AddText(fs, $"{Environment.NewLine}Quantidade de vendedores: {_salesmanList.Count}");
                AddText(fs, $"{Environment.NewLine}ID da venda mais cara: {_saleList.OrderByDescending(s => s.Total).FirstOrDefault().CodSale}");

                var groupSalesman = from sale in _saleList
                                    group sale by sale.SalesmanName into salesman
                                    select new
                                    {
                                        SalesmanName = salesman.Key,
                                        TotalSale = salesman.Sum(s => s.Total)
                                    };

                var worstSalesman = groupSalesman.OrderBy(s => s.TotalSale).FirstOrDefault();

                AddText(fs, $"{Environment.NewLine}O pior vendedor: {worstSalesman.SalesmanName} com R${worstSalesman.TotalSale}");
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void MoveProcessedFile(string dirFile)
        {
            if (!Directory.Exists(GetDirectoryPath("Processed")))
                Directory.CreateDirectory(GetDirectoryPath("Processed"));

            File.Move(dirFile, $"{GenerateFileAlias("Processed", Path.GetFileName(dirFile))}");
        }

        private string GenerateFileAlias(string directoryKey, string fileName) => $"{GetDirectoryPath(directoryKey)}/{DateTime.Now.ToString("ddMMyyyy_HHmmss")}_{fileName}";
        #endregion
    }
}
