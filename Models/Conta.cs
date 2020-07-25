using System;


namespace Banco.Models
{
    public class Conta
    {
        public Conta(double saldo, string numeroConta, double valorDeposito, double sacarValor)
        {
            Saldo = saldo;
            NumeroConta = numeroConta;
            ValorDeposito = valorDeposito;
            SacarValor = sacarValor;
            
        }
        
        public int Id { get; set; }
        public double Saldo { get; set; }
        public string NumeroConta { get; set; }
        public double ValorDeposito { get; set; }
        public double SacarValor { get; set; }
        

    }
}
