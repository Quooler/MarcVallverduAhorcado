using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarcVallverduAhorcado
{
    public partial class _Default : Page
    {
        static string palabraSecreta = "DINOSAURIO";
        List<char> letrasPalabraSecreta = palabraSecreta.ToList();

        int contadorAhorcado
        {
            get
            {

                return (int)ViewState["contadorAhorcado"];
            }
            set
            {
                ViewState["contadorAhorcado"] = value;
            }
        }
        int contadorIntentos
        {
            get
            {

                return (int)ViewState["contadorIntentos"];
            }
            set
            {
                ViewState["contadorIntentos"] = value;
            }
        }
        List<char> listPalabraDescubierta
        {
            get
            {
                if(ViewState["palabra"] == null)
                {
                    ViewState["palabra"] = new List<char>();
                }
                return ViewState["palabra"] as List<char>;
            }
            set
            {
                ViewState["palabra"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IniciarJuego();
            }
            else
            {
            }
        }

        protected void IniciarJuego()
        {
            contadorAhorcado = 0;
            contadorIntentos = 8;
            listPalabraDescubierta = new List<char>();
            labLetrasUsadas.Text = "Letras jugadas: ";

            foreach (char letra in letrasPalabraSecreta)
                listPalabraDescubierta.Add('_');

            ActualizarPalabraDescubierta();
        }

        protected void ActualizarPalabraDescubierta()
        {
            labContadorIntentos.Text = "Intentos restantes: " + contadorIntentos.ToString();
            imagenAhorcado.ImageUrl = "resources/ahorcado" + contadorAhorcado + ".png";
            labPalabraDescubierta.Text = "";
            txbLetraSeleccionada.Text = "";
            txbLetraSeleccionada.Focus();

            foreach (char letra in listPalabraDescubierta)
                labPalabraDescubierta.Text += letra + " ";
        }

        protected void txbLetraSeleccionada_TextChanged(object sender, EventArgs e)
        {
            if (txbLetraSeleccionada.Text.Length == 1)
                ComprobarLetraIntroducida();
        }

        protected void ComprobarLetraIntroducida()
        {
            bool acierto = false;

            //Comprobar en MAYUS la letra introducida, si és correcta se añade a la solución
            for (int i = 0; i < letrasPalabraSecreta.Count; i++)
                if (Convert.ToChar(txbLetraSeleccionada.Text.ToUpper()).Equals(letrasPalabraSecreta[i]))
                {
                    listPalabraDescubierta[i] = letrasPalabraSecreta[i];
                    acierto = true;
                }

            if (!acierto)
            {
                contadorIntentos--;
                contadorAhorcado++;

                if (contadorIntentos == 0)
                {
                    Response.Write("<script>alert('Has perdido!')</script>");
                    IniciarJuego();
                }
            }

            int contadorPalabrasCorrectas = 0;

            for (int i = 0; i < listPalabraDescubierta.Count; i++)
                if (listPalabraDescubierta[i].Equals(letrasPalabraSecreta[i]))
                    contadorPalabrasCorrectas++;

            if (contadorPalabrasCorrectas >= 10)
            {
                Response.Write("<script>alert('Has ganado! La palabra era DINOSAURIO')</script>");
                IniciarJuego();
            }

            labLetrasUsadas.Text += txbLetraSeleccionada.Text + " ";
            ActualizarPalabraDescubierta();
        }
    }
}