using System;
using System.Collections.Generic;
using System.Drawing;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CreditCardNote.Infrastructrue.Common;
using CreditCardNote.Infrastructrue.ViewModel;
using CreditCardNote.PlatformInterface;
using CreditCardNote.SQLite;
using CreditCardNote.SQLite.Model;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCardNote.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCreditCard : ContentPage
    {
        public CreditCard CreditCard { set; get; }
        public bool IsAdd { set; get; }

        public event EventHandler AddAction;
        public AddCreditCard()
        {
            InitializeComponent();
            foreach (var item in BankType.BankTypeList)
            {
                BankTypeList.Items.Add(item);
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (IsAdd)
            {
                Title = "添加";
                CreditCard = new CreditCard();
            }
            else
            {
                Title = "编辑";
                //CreditCard = new CreditCard();
            }
            BindingContext = CreditCard;
        }

        private async void Button_CameraClicked(object sender, EventArgs e)
        {
            MediaFile photo = null;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (PermissionStatus.Granted == status)
                {
                    photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {
                        CompressionQuality = 1,
                        CustomPhotoSize = 1
                    });
                }
                else
                {
                    DependencyService.Get<IToast>().ShortAlert("没有相机的权限");
                    return;
                }

            }
            catch (Exception exception)
            {
                await DisplayAlert("出错了", exception.Message, "确定");
            }
            if (photo != null)
            {
                var stream = photo.GetStream();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                string pic = Convert.ToBase64String(bytes);
                pic = HttpUtility.UrlEncode(pic);
                string postString = $"image={pic}";
                byte[] postData = Encoding.UTF8.GetBytes(postString);
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var tokenResponse = await webClient.UploadDataTaskAsync("https://aip.baidubce.com/oauth/2.0/token", "POST", Encoding.UTF8.GetBytes("grant_type=client_credentials&client_id=ClKHixnqs0HsEFQ5vsdqfQgH&client_secret=gSsGt3IGQMGCWzKnkLucejOcXGxIuV5H"));
                var tokenModel = JsonConvert.DeserializeObject<TokenModel>(Encoding.UTF8.GetString(tokenResponse));
                string url = "https://aip.baidubce.com/rest/2.0/ocr/v1/bankcard?access_token=" + tokenModel.access_token;//地址  
                CallBankApiResoponseModel model = null;
                try
                {
                    byte[] responseData = await webClient.UploadDataTaskAsync(url, "POST", postData);//得到返回字符流  
                    string srcString = Encoding.UTF8.GetString(responseData);//解码  
                    model = JsonConvert.DeserializeObject<CallBankApiResoponseModel>(srcString);
                    if (model != null && model.result != null)
                    {
                        CreditCard.CardNumber = model.result.bank_card_number;
                        CreditCard.BankName = model.result.bank_name;
                        CreditCard.BankCardType = model.result.bank_card_type;
                    }
                    else
                    {
                        await DisplayAlert("图片数据出错了", model.error_msg, "确定");
                    }
                }
                catch (Exception exception)
                {
                    await DisplayAlert("出错了", exception.Message, "确定");
                }
            }
        }

        private void Button_SaveClicked(object sender, EventArgs e)
        {
            var model = (CreditCard)BindingContext;
            if (Common.ModelIsNull<CreditCard>(model))
            {
                DependencyService.Get<IToast>().ShortAlert("不能有空值");
                return;
            }
            if (!(model.RepaymentDate >= 1 || model.RepaymentDate <= 31))
            {
                DependencyService.Get<IToast>().ShortAlert("还款日输入错误");
                return;
            }
            if (!(model.StatementDate >= 1 || model.StatementDate <= 31))
            {
                DependencyService.Get<IToast>().ShortAlert("账单日输入错误");
                return;
            }
            model.CardNumber = model.CardNumber.Trim();
            AddAction?.Invoke(BindingContext, e);
            DependencyService.Get<IToast>().ShortAlert("添加成功");
            Navigation.PopAsync(true);
        }
    }
}