<%@ Page Title="" Language="C#" Debug="true" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="XRetailManagerUI.Pages.Sales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
         
        <div class="container" style="margin-top:30px">
            <div class="row">
                <div class="col-md-4">
                               <h2>Items for Sale</h2>
            <div style="height: 600px; overflow-y: scroll;">
                <asp:Repeater ID="lstProducts" runat="server" OnItemCommand="lstProducts_ItemCommand" >
                    <ItemTemplate>
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title"><%# Eval("ProductName") %></h4>
                                <p class="card-text">Description: <%# Eval("Description") %></p>
                                <p class="card-text">Quantity: <%# Eval("QuantityInStock") %></p>
                                <asp:Button ID="btnAddToCart" runat="server" Text="Select Item" CommandName="AddToCart" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-success" />
                            </div>
                        </div>
                        <hr />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
                <div class="col-md-4">
                    <h2>Add to Cart</h2>
                     <asp:Label ID="lblSelectedItem" runat="server" ForeColor="Green" />
                       <asp:Button ID="btnRemoveSelectedItem" Text="X" runat="server" ForeColor="Red" Visible="false" OnClick="btnRemoveSelectedItem_Click" />
                    <br />
                       <asp:Label ID="lblQuantityValidation" runat="server" ForeColor="Red" />
                       <br />
                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantity" type="number" value="1" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" CssClass="btn btn-primary" />
                    <br />
                    <br />
                    <asp:Button ID="btnRemoveFromCart" runat="server" Text="Remove from Cart" OnClick="btnRemoveFromCart_Click" CssClass="btn btn-danger" />
                </div>
                <div class="col-md-4">
                    <h2>Cart</h2>
                      <%-- <asp:Label ID="lblCartItems" runat="server" ForeColor="Green" />--%>
                       <asp:Button ID="btnRemoveAllItemsFromCart" Text="X Remove All Cart Items" runat="server" class="btn btn-outline-danger" Visible="false" OnClick="btnRemoveAllItemsFromCart_Click" />
                    <br />
                    <br />
                    <asp:ListBox ID="lstCart" runat="server" CssClass="form-control"></asp:ListBox>
                    <br />
                    <asp:Label ID="lblSubTotal" runat="server" Text="SubTotal:" CssClass="font-weight-bold"></asp:Label>
                    <br />
                    <asp:Label ID="lblTax" runat="server" Text="Tax:" CssClass="font-weight-bold"></asp:Label>
                    <br />
                    <asp:Label ID="lblTotal" runat="server" Text="Total:" CssClass="font-weight-bold"></asp:Label>
                     <br />
                    <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" OnClick="btnCheckOut_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
   
    </main>
</asp:Content>
