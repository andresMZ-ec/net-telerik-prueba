---
title: Notificaciones
description: Detalle técnico para corrección o extender funcionalidad en componente de notificación.
---


- `Notificacion.ascx`: Diseño del componente, style o script
- `Notificacion.ascx.vb`: Métodos del componente 

- `Web.config`

  Registro del componente *Notificación* para ser accedido de forma global:
  ```
  <system.web>
    ...
    <controls>
      ...
      <add tagPrefix="componentes" tagName="Notificacion" src="~/Content/Componentes/Notificacion.ascx" />
      ...
    </controls>
    ...
  </system.web>
  ```
 
- `Site.Master`

  Incluir el componente al Site.Master
  ```
    <form runat="server">
      ...
      <componentes:Notificacion ID="Notificacion" runat="server" />
      ...
    </form>
  ```

  En el code-behind del Site.Master, añadir una nueva propiedad de forma pública para ser utilizada en cualquier página hija. Sin esa propiedad el componente no se referencia en otro `.aspx` o `.ascx`

  ```
    Public ReadOnly Property Notification() As Notificacion
      Get
          Return Me.FindControl("Notificacion")
      End Get
    End Property
  ```