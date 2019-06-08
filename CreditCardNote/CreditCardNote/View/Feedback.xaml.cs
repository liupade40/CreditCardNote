using CreditCardNote.PlatformInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCardNote.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Feedback : ContentPage
    {
        public Feedback()
        {
            InitializeComponent();
        }
        public void Submit(object sender, EventArgs e)
        {
            if (Content.Text == null || Content.Text.Length < 10)
            {
                DependencyService.Get<IToast>().ShortAlert("请输入十个以上的字符！");
                return;
            }
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();
                //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
                mailMessage.From = new MailAddress("819414741@qq.com");
                //收件人邮箱地址。
                mailMessage.To.Add(new MailAddress("819414741@qq.com"));
                //邮件标题。
                mailMessage.Subject = "信用卡管家App反馈";
                //邮件内容。
                mailMessage.Body = Content.Text;

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();
                //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
                client.Host = "smtp.qq.com";
                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential("819414741@qq.com", "rslybmvvpbuibefe");
                //发送
                client.Send(mailMessage);
                DependencyService.Get<IToast>().ShortAlert("发送成功");
                Content.Text = "";
            }
            catch (Exception)
            {

                DependencyService.Get<IToast>().ShortAlert("发送失败");
            }

        }
    }
}