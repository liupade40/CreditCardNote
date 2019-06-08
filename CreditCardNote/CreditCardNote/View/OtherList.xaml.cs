using CreditCardNote.SQLite.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCardNote.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherList : ContentPage
    {
        public OtherList()
        {
            InitializeComponent();
            //todo: 后期是否需要添加通知列表，判断今天是不是还款日的通知
            listView.ItemsSource = new string[] { "使用流程", "声明", "建议与反馈", "清除数据", "版本", };
        }
        public async Task OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var text = e.SelectedItem as string;
            if (text==null)
            {
                return;
            }
            switch (text)
            {
                case "使用流程":
                    await DisplayAlert("使用流程", "1、添加信用卡，信用卡号和银行还有银行发送消费短信的号码（一般是95之类的）这三个必须正确才能获取到短信。\r\n2、App会获取短信。\r\n3、可以查看消费短信和信用卡的统计。\r\n4、暂时只支持招商、广发、交行、民生、华夏、中信银行短信识别，其他银行未测试。", "确定");
                    break;
                case "声明":
                    await DisplayAlert("声明", "本App所有的数据都保存在本地，不会和服务器交互，如果卸载或者删除数据都无法恢复数据，添加信用卡默认只获取近两个月的短信", "确定");
                    break;
                case "建议与反馈":
                    await Navigation.PushAsync(new Feedback());
                    break;
                case "清除数据":
                    var b = await DisplayAlert("提示", "确定删除所有数据?", "确定", "取消");
                    if (b)
                    {
                        MessageList.BillList.Clear();
                        CreditCardList.CreditCard.Clear();
                        SQLiteAccessHelper.DeleteTable();
                    }
                    break;
                case "版本":
                    await DisplayAlert("版本", "信用卡笔记 V1.1.0", "确定");
                    break;
            }
            listView.SelectedItem = null;
        }

    }
}