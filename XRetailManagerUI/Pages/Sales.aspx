<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="XRetailManagerUI.Pages.Sales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
         
        <div class="container" style="margin-top:30px">
            <div class="row">
                <div class="col-md-4">
                    <h2>Items for Sale</h2>
                    <asp:ListBox ID="lstItems" runat="server" CssClass="form-control"></asp:ListBox>
                </div>
                <div class="col-md-4">
                    <h2>Add to Cart</h2>
                    <br />
                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" CssClass="btn btn-primary" />
                    <br />
                    <br />
                    <asp:Button ID="btnRemoveFromCart" runat="server" Text="Remove from Cart" OnClick="btnRemoveFromCart_Click" CssClass="btn btn-danger" />
                </div>
                <div class="col-md-4">
                    <h2>Cart</h2>
                    <asp:ListBox ID="lstCart" runat="server" CssClass="form-control"></asp:ListBox>
                    <br />
                    <asp:Label ID="lblSubTotal" runat="server" Text="SubTotal:" CssClass="font-weight-bold"></asp:Label>
                    <br />
                    <asp:Label ID="lblTax" runat="server" Text="Tax:" CssClass="font-weight-bold"></asp:Label>
                    <br />
                    <asp:Label ID="lblTotal" runat="server" Text="Total:" CssClass="font-weight-bold"></asp:Label>
                </div>
            </div>
        </div>
   
    </main>
</asp:Content>
