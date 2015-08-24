   using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//new add
using System.Diagnostics;
using System.Threading;
using LuaInterface;
using System.IO;

namespace ScriptRun
{

    public partial class ScriptRunner : Form
    {
        Thread threadLuaRunner;

        public ScriptRunner()
        {
            InitializeComponent();
        }

        public void OutputLine(string text)
        {
            System.Console.WriteLine(text);
        }
        public void Sleep(int time)
        {
            Thread.Sleep(time);
        }
        public void FMSignal(int channel, float carrier, float low, float offset, float ampl)
        {
            AFG3022.GetInstance().FMSignal(channel, carrier, low, offset, ampl);
        }

        public void Single(int channel, float freq, float ampl)
        {
            AFG3022.GetInstance().Single(channel, freq, ampl);
        }
        public void Output(int channel, bool state)
        {
            AFG3022.GetInstance().Output(channel, state);
        }

        private void RunLua(object obj)
        {
            try
            {
                string luaFile = obj as string;
                Lua luaVM = new Lua();

                luaVM.RegisterFunction("FMSignal", this, this.GetType().GetMethod("FMSignal"));
                luaVM.RegisterFunction("Single", this, this.GetType().GetMethod("Single"));
                luaVM.RegisterFunction("Output", this, this.GetType().GetMethod("Output"));
                luaVM.RegisterFunction("OutputLine", this, this.GetType().GetMethod("OutputLine"));
                luaVM.RegisterFunction("Sleep", this, this.GetType().GetMethod("Sleep"));

                luaVM.DoFile(luaFile);

                luaVM.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                this.Invoke(new MethodInvoker(delegate
                {

                    this.lua_button.Text = "运行脚本";
                }));
            }

        }

        private void lua_button_Click(object sender, EventArgs e)
        {

            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }



                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "LuaScript|*.lua";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                    threadLuaRunner.IsBackground = true;
                    threadLuaRunner.Start(ofd.FileName);
                    this.lua_button.Text = "停止脚本";

                }

            }
            else
            {
                threadLuaRunner.Abort();
                this.lua_button.Text = "Run Lua";

            }

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            float carrier = float.Parse(textBox1.Text);
            float lowFreq = float.Parse(textBox2.Text);
            float freqDiff = float.Parse(textBox3.Text);
            float ampl = float.Parse(textBox4.Text);
            AFG3022.GetInstance().FMSignal(1, carrier, lowFreq, freqDiff, ampl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float freq = float.Parse(textBox5.Text);
            float ampl = float.Parse(textBox6.Text);
            AFG3022.GetInstance().Single(1, freq, ampl);
        }

        private void 点灯电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\交流信号机电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"交流信号机电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }

                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }


            }
             
        }

        private void 灯端电压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\交流信号机电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"交流信号机电压.lua\"脚本不存在！");
                    return;
                }
                

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 点灯电流ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\直流信号机电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"直流信号机电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 灯端电压ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\直流信号机电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"直流信号机电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void hz单频交流IToolStripMenuItem_Click(object sender, EventArgs e) //25HzI
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\25Hz单频交流电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"25Hz单频交流电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void hz单频交流IToolStripMenuItem1_Click(object sender, EventArgs e) //50HzI
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\50Hz单频交流电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"50Hz单频交流电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 调制交流IToolStripMenuItem_Click(object sender, EventArgs e) //550-850HzI
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\550-850调制交流电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"550-850调制交流电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 调制交流IToolStripMenuItem1_Click(object sender, EventArgs e) //1700-2600HzI
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\1700-2600调制交流电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"1700-2600调制交流电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void hz单频交流UToolStripMenuItem_Click(object sender, EventArgs e) //25HzU
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\1700-2600调制交流电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"1700-2600调制交流电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void hz单频交流UToolStripMenuItem1_Click(object sender, EventArgs e) //50HzU
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\50Hz单频交流电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"50Hz单频交流电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 调制交流UToolStripMenuItem_Click(object sender, EventArgs e) //550-850HzU
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\550-850调制交流电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"550-850调制交流电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 调制交流UToolStripMenuItem1_Click(object sender, EventArgs e)//1700-2300HzU
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\1700-2600调制交流电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"1700-2600调制交流电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 动作电流ToolStripMenuItem_Click(object sender, EventArgs e)//交流
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\交流道岔动作电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"交流道岔动作电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 动作电压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\交流道岔动作电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"交流道岔动作电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 表示电压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\交流道岔表示电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"交流道岔表示电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 动作电流ToolStripMenuItem1_Click(object sender, EventArgs e)//直流
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\直流道岔动作电流.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"直流道岔动作电流.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 动作电压ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\直流道岔动作电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"直流道岔动作电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }

        private void 表示电压ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (threadLuaRunner == null || (threadLuaRunner.IsAlive == false))
            {
                string fileName = Environment.CurrentDirectory;
                fileName = fileName + "\\脚本\\直流道岔表示电压.lua";
                if (System.IO.File.Exists(fileName) == false)
                {
                    MessageBox.Show("\"直流道岔表示电压.lua\"脚本不存在！");
                    return;
                }

                if (false == HelperApi.ConsoleShow.consoleState)
                {
                    HelperApi.ConsoleShow pConsoleShow = new HelperApi.ConsoleShow();
                    pConsoleShow.CsAllocConsole();
                }
                else
                {
                    System.Console.WriteLine("Lua Running!");
                }
                //MessageBox.Show(fileName);
                threadLuaRunner = new Thread(new ParameterizedThreadStart(RunLua));

                threadLuaRunner.IsBackground = true;
                threadLuaRunner.Start(fileName);


            }
            else
            {
                DialogResult dr = MessageBox.Show("当前脚本正在运行，是否中止？", "消息内容", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    threadLuaRunner.Abort();
                }

            }
        }
    }
}
