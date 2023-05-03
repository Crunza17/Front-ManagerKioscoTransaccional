<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.Login" EnableEventValidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- 2022 - Touch Colombia - Todos los derechos reservados -->
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width"/>
        <!-- Font Awesome -->
<link
  href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"
  rel="stylesheet"
/>
<!-- Google Fonts -->
<link
  href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap"
  rel="stylesheet"
/>
<!-- MDB -->
<link
  href="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/4.0.0/mdb.min.css"
  rel="stylesheet"
/>
<script
  type="text/javascript"
  src="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/4.0.0/mdb.min.js"
></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio de Sesión</title>
</head>
    <style>
        body {
            background-color: #f5f5f5;
        }
        section {
            margin: 150px auto;
        }
    </style>
<body>
    <form id="form1" runat="server">
        <section class="w-75 h-50">
  <!-- Jumbotron -->
  <div class="px-4 py-5 px-md-5 text-center text-lg-start" style="background-color: hsl(0, 0%, 96%)">
    <div class="container">
      <div class="row gx-lg-5 align-items-center">
        <div class="col-lg-6 mb-5 mb-lg-0" title="© 2022 Copyright - Touch Colombia - Todos los derechos reservados">
          <h1 class="my-5 display-3 fw-bold ls-tight" >
            Bienvenido a <br /> 
            <span class="text-danger">TouchPay</span>Manager
          </h1>
          <p style="color: hsl(217, 10%, 50.8%)">
            Ingrese su documento y su contraseña.
          </p>
        </div>

        <div class="col-lg-6 mb-5 mb-lg-0">
          <div class="card">
            <div class="card-body py-5 px-md-5">
              <form>
                <!-- Email input -->
                <div class="form-outline mb-6">
                  <asp:TextBox ID="txtUser" class="form-control" runat="server" placeholder=""></asp:TextBox>
                  <label class="form-label text-danger" for="form3Example3">Identificación</label>
                </div>

                <!-- Password input -->
                <div class="form-outline mb-6">
                  <asp:TextBox ID="txtPass" class="form-control" runat="server" textMode="Password" placeholder=""></asp:TextBox>
                  <label class="form-label text-danger" for="form3Example4">Contraseña</label>
                </div>

                <!-- Submit button -->
                <asp:Button ID="btnIngresar" class="btn btn-danger btn-block mb-4" runat="server" Text="Ingresar" OnClick="btnIngresar_Click"/>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- Jumbotron -->
</section>
</form>
</body>
</html>
