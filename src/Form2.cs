using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 키오스크
{
    public partial class Form2 : Form
    {
        public const int MENU_NUM = 12; //메뉴 개수 설정
        Form1 form1;    //form1 불러오기
        
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 _form)
        {
            InitializeComponent();
            form1 = _form;
        }
        //----------취소 버튼-----------
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        //----------form1에서 listView 가져오기----------
        private void Form2_Load(object sender, EventArgs e)
        {
            foreach (ListViewItem item in form1.listView1.Items) 
            {
                this.listView1.Items.Add((ListViewItem)item.Clone());
                this.textBox1.Text = form1.textBox1.Text;
            }

        }
        //-----------결제 버튼------------
        private void button1_Click(object sender, EventArgs e)
        {
            //결제 창에서 Yes 클릭시
            if(MessageBox.Show("총액 : " + textBox1.Text + "\n결제하시겠습니까?", "결제 창", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("결제 완료");
                form1.n++;
                //파일에 결제 내역 추가 (몇 번째 주문인지, 메뉴 (이름,가격,개수), 총액, 날짜)
                FileStream fs;
                try
                {
                    fs = new FileStream("주문 내역.txt", FileMode.Append);
                }
                catch (IOException)
                {
                    Console.WriteLine("파일을 열 수 없습니다.");
                    return;
                }
                StreamWriter w = new StreamWriter(fs);
                w.WriteLine("===================" + form1.n + "번째 주문===================");
                for(int i = 0; i < listView1.Items.Count; i++)
                {
                    w.WriteLine("{0,-20}{1,-20}{2,-20}", listView1.Items[i].SubItems[0].Text, listView1.Items[i].SubItems[1].Text + "원", listView1.Items[i].SubItems[2].Text + "개");
                  }
                w.WriteLine("총액 : " + textBox1.Text);
                w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                w.Close();

                form1.listView1.Items.Clear();   //listView 비우기
                form1.total = 0;                 //총액 초기화
                form1.textBox1.Text = "0원";
                for (int i = 0; i < MENU_NUM; i++)  //메뉴 전체 개수 초기화
                    form1.menu[i, 2] = "0";
                Close();    //form2 닫기
            }
            //결제 창에서 No 클릭시
            else   
                MessageBox.Show("결제 취소");
        }
    }
}
