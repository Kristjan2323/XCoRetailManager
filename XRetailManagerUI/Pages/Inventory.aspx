<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="XRetailManagerUI.Pages.Inventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
          <div class="container">
        <h1>Inventory</h1>
              <div>
                  <asp:Label ID="lblInventory" runat="server" />
              </div>
        <div class="table-responsive">
            <asp:GridView ID="inventoryGrid" runat="server" CssClass="table table-striped table-bordered"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Product.ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="PurchasePrice" HeaderText="Purchase Price" />
                    <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" />
                </Columns>
            </asp:GridView>
        </div>
              
    </div>
    </main>

</asp:Content>
