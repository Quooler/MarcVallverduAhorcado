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

        int contadorPalabrasCorrectas
        {
            get
            {

                return (int)Session["contadorPalabrasCorrectas"];
            }
            set
            {
                Session["contadorPalabrasCorrectas"] = value;
            }
        }
        int contadorAhorcado
        {
            get
            {

                return (int)Session["contadorAhorcado"];
            }
            set
            {
                Session["contadorAhorcado"] = value;
            }
        }
        int contadorIntentos
        {
            get
            {

                return (int)Session["contadorIntentos"];
            }
            set
            {
                Session["contadorIntentos"] = value;
            }
        }
        List<char> listPalabraDescubierta
        {
            get
            {
                if(Session["palabra"] == null)
                {
                    Session["palabra"] = new List<char>();
                }
                return Session["palabra"] as List<char>;
            }
            set
            {
                Session["palabra"] = value;
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
            //Reiniciamos las variables con los valores iniciales
            contadorPalabrasCorrectas = 0;
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
                    contadorPalabrasCorrectas ++;
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