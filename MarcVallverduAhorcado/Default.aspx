<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MarcVallverduAhorcado._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row">
            <div class="col">
                <h1 id="aspnetTitle" class="p-b-500;">Guess the word</h1>
                <asp:Label ID="labPalabraDescubierta" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <asp:Label ID="labLetrasUsadas" runat="server" Text="Letras jugadas: "></asp:Label>
            </div>
            <div class="col">
                <asp:Image id="imagenAhorcado" ImageUrl="resources/ahorcado0.png" alt="ahorcado" width="250px" runat="server"/>
            </div>
        </section>

        <section class="row">
            <div class="col">
                <asp:TextBox ID="txbLetraSeleccionada" runat="server" OnTextChanged="txbLetraSeleccionada_TextChanged" AutoPostBack="true" MaxLength="1"></asp:TextBox>  
                <asp:Label ID="labContadorIntentos" runat="server" Text=""></asp:Label>
            </div>
        </section>
    </main>

</asp:Content>
