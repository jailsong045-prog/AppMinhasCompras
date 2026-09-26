using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        public ListaProduto()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                lst_produtos.ItemsSource = await App.Db.GetAll();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Navega para a tela de adicionar novo produto
                await Navigation.PushAsync(new NovoProduto());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    Produto p = e.SelectedItem as Produto;
                    await Navigation.PushAsync(new EditarProduto
                    {
                        BindingContext = p
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                Produto p = mi.CommandParameter as Produto;

                bool confirm = await DisplayAlert("Confirmação", $"Deseja remover {p.Descricao}?", "Sim", "Não");

                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    lst_produtos.ItemsSource = await App.Db.GetAll();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}