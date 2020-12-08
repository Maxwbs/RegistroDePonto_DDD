using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace Entities.Notificacoes
{
    public class Notifica
    {
        private static DateTime _limiteInferior = DateTime.Parse("1/1/1753", CultureInfo.InvariantCulture);
        private static DateTime _limiteSuperior = DateTime.Parse("12/31/9999", CultureInfo.InvariantCulture);

        public Notifica()
        {
            notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifica> notificacoes;

        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {

            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValideTamanhoPropriedade(string valor, string nomePropriedade, int tamanhoMaximo, int tamanhoMinimo)
        {

            if (!(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade)))
            {
                var tamanhoValor = valor.Length;

                if (!(tamanhoValor >= tamanhoMinimo && tamanhoValor <= tamanhoMaximo))
                {
                    notificacoes.Add(new Notifica
                    {
                        mensagem = "Tamanho do campo inválido",
                        NomePropriedade = nomePropriedade
                    });
                }

                return false;
            }

            return true;
        }

        public bool ValideValorPropriedade(int valor, string nomePropriedade, int valorMaximo, int valorMinimo)
        {

            var tamanhoValor = valor;

            if (!(tamanhoValor >= valorMinimo && tamanhoValor <= valorMaximo))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "valor do campo inválido",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValideValorPropriedadeEnum(Type typeEnum, string valor, string nomePropriedade)
        {
            var valoresValidos = (IList)Enum.GetValues(typeEnum);
            var existeEnum = false;

            for (int i = 0; i < valoresValidos.Count; i++)
            {
                var valorEnum = valoresValidos[i];

                if (valorEnum.ToString().ToUpper() == valor.ToUpper())
                {
                    existeEnum = true;
                    break;
                }
            }

            if (!existeEnum)
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "valor do campo inválido",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadeInt(int valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = "nomePropriedade"
                });

                return false;
            }

            return true;

        }
        public bool ValidaPropriedadeCpf(string valor, string nomePropriedade)
        {

            if (!string.IsNullOrWhiteSpace(valor) && !string.IsNullOrWhiteSpace(nomePropriedade) && !CpfEhValido(valor))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "Cpf inválido",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadeDateTime(DateTime valor, string nomePropriedade)
        {
            if (!EhDataValida(valor))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo Data é inválida",
                    NomePropriedade = nomePropriedade
                });
            }

            return !notificacoes.Any();
        }

        private static bool EhDataValida(DateTime data)
        {
            return data >= _limiteInferior && data <= _limiteSuperior;
        }
        public static bool CpfEhValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;

            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

    }

}
