using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 키오스크
{
    public partial class Form1 : Form
    {
        public const int MENU_NUM = 12; //메뉴 개수 설정
        public int n = 0;   //주문 횟수
        //2차원 배열 메뉴 이름, 가격, 개수
        public string[,] menu = new string[,] { { "1955 버거", "5700", "0" }, { "빅맥", "4600", "0" }, { "슈슈 버거", "4500", "0" }, { "슈비 버거", "5500", "0" } ,{ "불고기 버거", "2200", "0" },
                                                { "후렌치 후라이", "1700","0"},{ "치즈스틱", "2200","0"},{ "아이스크림콘", "700","0" }, {"선데이 아이스크림", "1500","0" },
                                                { "코카 콜라", "1400","0" }, { "스프라이트", "1400","0" },{ "환타", "1400","0" }};
        public int total = 0;   //총액
        public Form1()
        {
            InitializeComponent();
        }
        private void AddList(string name)
        {
            int n = -1;
            //선택한 메뉴를 배열에서 찾음
            for (int i = 0; i < MENU_NUM; i++) {
                if (menu[i, 0] == name) {
                    n = i;      //배열 번호 저장
                    break;
                }
            }
            //이미 listView에 있는 메뉴면 개수 증가
            for(int i = 0; i < listView1.Items.Count; i++) {
                if (listView1.Items[i].Text == name) {
                    menu[n, 2] = (int.Parse(menu[n, 2]) + 1).ToString();   //개수 증가
                    listView1.Items[i].SubItems[2].Text = menu[n, 2];
                    total += int.Parse(menu[n, 1]);     //총액 증가
                    textBox1.Text = total.ToString() + "원";
                    return;
                }
            }
            //메뉴 새로 listView에 추가
            ListViewItem lvi = new ListViewItem(menu[n, 0]);
            lvi.SubItems.Add(menu[n, 1]);
            menu[n, 2] = (int.Parse(menu[n, 2]) + 1).ToString();    //개수 증가
            lvi.SubItems.Add(menu[n, 2]);
            listView1.Items.Add(lvi);

            total += int.Parse(menu[n, 1]);
            textBox1.Text = total.ToString() + "원";
        }
        //----------탭 전환-----------
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        //-----------버거------------
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddList("1955 버거");
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddList("빅맥");
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AddList("슈슈 버거");
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AddList("슈비 버거");
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            AddList("불고기 버거");
        }
        //-----------사이드-----------
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            AddList("후렌치 후라이");
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            AddList("치즈스틱");
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            AddList("아이스크림콘");
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            AddList("선데이 아이스크림");
        }
        //----------음료-------------
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            AddList("코카 콜라");
        }
        private void pictureBox16_Click(object sender, EventArgs e)
        {
            AddList("스프라이트");
        }
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            AddList("환타");
        }
        //----------메뉴 취소-----------
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                //배열에서 메뉴 찾기
                for (int i = 0; i < MENU_NUM; i++)
                {
                    if (menu[i,0] == listView1.SelectedItems[0].Text)
                    {
                        total -= int.Parse(menu[i, 1]) * int.Parse(menu[i, 2]);     //총액에서 취소할 메뉴 가격*개수 빼주기
                        textBox1.Text = total.ToString() + "원";
                        menu[i,2]= "0";     //개수 0으로 초기화
                        break;
                    }
                }
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);     //listView에서 취소 메뉴 삭제
            }
        }
        //--------결제 버튼------------
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("메뉴를 담아주세요!");
                return;
            }
            Form2 form2 = new Form2(this);
            form2.ShowDialog();     //모달 방식으로 Form2 열기
        }
        //--------비우기 버튼-----------
        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();    //listView 비우기
            for(int i = 0; i < MENU_NUM; i++)
                menu[i, 2] = "0";   //전체 메뉴 개수 0으로 초기화
            total = 0;
            textBox1.Text = "0원";   //총액 0으로 초기화
        }
    }
}
