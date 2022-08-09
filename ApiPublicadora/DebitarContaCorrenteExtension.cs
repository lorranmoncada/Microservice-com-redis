namespace ApiPublicadora
{
    public static class DebitarContaCorrenteExtension
    {
        public static string DebitarContaCorrente(this DebitoConta conta)
        {
            conta.Valor = conta.Valor - (decimal)0.2;

            conta.FormatarValor();

            return "Valor debitado conta Corrente";
        }
    }
}
