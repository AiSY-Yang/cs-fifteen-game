using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 排块游戏
{
    public partial class Form1 : Form
    {
        //初始化为4*4个按钮
        const int N = 4;
        Button[,] buttons = new Button[N, N];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //游戏开始
            //创建所有按钮
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Button button = new Button();
                    button.Text = (i * N + j + 1).ToString();
                    button.Top = 50 * i;
                    button.Left = 50 * j;
                    button.Width = 40;
                    button.Height = 40;
                    button.Tag = i * N + j;
                    button.Visible = true;
                    buttons[i, j] = button;
                    //按钮加入窗口中
                    this.Controls.Add(button);
                    //注册事件
                    button.Click += numberButton_Click;
                }
            }
            //最后一个按钮text为空
            buttons[N - 1, N - 1].Text = "";
        }
        private void buttonGameStart_Click(object sender, EventArgs e)
        {
            //打乱顺序
            Random random = new Random();
               for (int i = 0; i < 100; i++)
                    swap(buttons[random.Next(N), random.Next(N)], buttons[random.Next(N), random.Next(N)]);
        }
        private void numberButton_Click(object sender, EventArgs e)
        {
            //点击除开始外任意数字调用
            //当前点击方块和空方块的位置
            Button btn = sender as Button;
            Button blank = findblank();
            //判断是不是找到了空方块
            if (blank.Tag == null)
            {
                raise("blank not found");
            }
            //是否相邻，若不相邻则不执行任何操作
            if (!isNeighbor(btn, blank))
            {
                return;
            }
            swap(btn, blank);
            //判断是否完成
            if (isOK())
            {
                win();
                return;
            }
        }

        public bool isNeighbor(Button btnA, Button btnB)
        {
            //行列信息
            int rowA = (int)btnA.Tag / N;
            int rowB = (int)btnB.Tag / N;
            int columuA = (int)btnA.Tag % N;
            int columuB = (int)btnB.Tag % N;
            //同一行不同列
            if ((Math.Abs(rowA - rowB) == 0) && (Math.Abs(columuA - columuB) == 1))
                return true;
            else
            //同一列不同行
                if ((Math.Abs(rowA - rowB) == 1) && (Math.Abs(columuA - columuB) == 0))
                return true;
            else
                return false;

        }
        bool isOK()
        {
            //判断是否完成 可以改为foreach in
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == N - 1 && j == N - 1)
                        return true;
                    if (buttons[i, j].Text != ((int)buttons[i, j].Tag+1).ToString())
                        return false;
                }
            }
            return true;
        }
        public void swap(Button a, Button b)
        {
            //交换text属性
            string temp;
            temp = a.Text;
            a.Text = b.Text;
            b.Text = temp;

        }
        public Button findblank()
        {
            //寻找空白方块
            foreach (Button btn in buttons)
            {
                if (btn.Text == "")
                {
                    return btn;
                }
            }
            return new Button();
        }
        public void win()
        {
            MessageBox.Show("You are win");
        }
        public void raise(string str)
        {
            MessageBox.Show(str, "raise");
        }
    }
}
