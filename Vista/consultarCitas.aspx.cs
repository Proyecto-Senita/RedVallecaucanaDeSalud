﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Vista
{
    public partial class consultarCitas1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClsCita clsCita = new ClsCita();
                if (int.Parse(Session["idRol"].ToString()) == 3)
                {
                    gdgGrid.DataSource = clsCita.ConsultarCita(int.Parse(Session["idUsuario"].ToString()), "Reservada");
                }
                else
                {
                    gdgGrid.DataSource = clsCita.AllConsulCita();
                }
                gdgGrid.DataBind();
                //gdgGrid.HeaderRow.TableSection = TableRowSection.TableHeader; // Agrega etiqueta: <thead> a la tabla
                mostraPanel(2);
            }

        }

        protected void btnCalificar_Click(object sender, EventArgs e)
        {
            ClsCita clsCita = new ClsCita();
            Cita cita = new Cita();
            cita.id_cita = int.Parse(TextIDcita.Text);
            cita.calificacion = int.Parse(TextCalificacion.Text);
            clsCita.CalificarCita(cita.calificacion.Value, cita);
            mostraPanel(2);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        public void mostraPanel(int panel)
        {
            PanelForm.Visible = panel == 1;
            PanelGrid.Visible = panel == 2;
        }
        protected void gdgGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow filaSeleccionada = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = filaSeleccionada.RowIndex;
            if (e.CommandName == "Calificar")
            {
                TextIDcita.Text = gdgGrid.Rows[rowIndex].Cells[0].Text;
                TextFecha.Text = gdgGrid.Rows[rowIndex].Cells[1].Text;
                TextHora.Text = gdgGrid.Rows[rowIndex].Cells[2].Text;
                mostraPanel(1);
            }
        }

        protected void gdgGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (int.Parse(Session["idRol"].ToString()) != 3)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                    e.Row.Cells[7].Visible = false;
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Cells[7].Visible = false;
            }
        }


        protected void gdgGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}