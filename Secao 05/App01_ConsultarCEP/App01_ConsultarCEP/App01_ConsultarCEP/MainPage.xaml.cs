using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Service.Model;
using App01_ConsultarCEP.Service;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPService.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {3} {0}, {1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O Endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
            else
            {
                RESULTADO.Text = string.Empty;
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valid = true;
            int novoCEP = 0;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "Cep inválido! O CEP deve conter 8 caracteres", "OK");

                valid = false;
            }

            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("ERRO", "Cep inválido! O CEP deve ser composto apenas por números", "OK");

                valid = false;
            }

            return valid;
        }
	}
}
