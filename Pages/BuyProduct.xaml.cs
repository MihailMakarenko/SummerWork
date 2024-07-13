using static System.Net.Mime.MediaTypeNames;

namespace SummerWorkProject.Pages;

public partial class BuyProduct : ContentPage
{
    private void AddProduct(Product product)
    {

    }


    public BuyProduct()
    {
        InitializeComponent();
        DateBuy.Date = DateTime.Now;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        PickerTypeProduct.ItemsSource = await TypeProduct.GetProducts();

        // ����� ����� ��������� ������ �������� ����� ��������� ������
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {

        if (!CheckValue())
        {
            await DisplayAlert("�����������", "�� ��� ���� ���������", "����������");
        }
        else
        {
            Product product = new Product(double.Parse(PriceEntry.Text), DateBuy.Date, PickerTypeProduct.SelectedItem.ToString(), (byte)CountProduct.Value, Place.Text, Title.Text, Description.Text);
            await product.UploadDataInTxtFile();
            text.Text = product.ToString();
            var choice = await DisplayAlert("�����������", "������ ���������", "����������", "���������");

            if (choice)
            {
                ClearnField();
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }

    private void PickerSelectedIndexChanged(object sender, DateChangedEventArgs e)
    {
        // ��������� ��������� ����
    }

    void DateSelected(object sender, DateChangedEventArgs e)
    {

    }

    void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        countProduct.Text = $"�������: {e.NewValue:F1}";
    }

    bool CheckValue()
    {
        if (PriceEntry.Text == null || PickerTypeProduct.SelectedIndex == -1)
        {
            return false;
        }
        return true;
    }

    void ClearnField()
    {
        PriceEntry.Text = null;
        CountProduct.Value = 1;
        Title.Text = null;
        Description.Text = null;
    }


}