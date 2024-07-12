using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UndoRedoManager;

namespace Tester
{
    public partial class Form1 : Form, IMementoOriginator<Memento>
    {
        public Form1()
        {
            InitializeComponent();
            this._undoredo=new UndoRedoManagerLimitHistory<Memento>(this,3);
            this.SetBtnEnable();
        }
        IUndoRedoble _undoredo;
        public Memento CreateMemento()
        {
            return new Memento(this.textBox1.Text);
        }

        public void SetMemento(Memento memento)
        {
            this.textBox1.Text = memento.Value;
            this.SetBtnEnable();
        }
        public void SetBtnEnable()
        {
            this.BtnRedo.Enabled = this._undoredo.UseRedo;
            this.BtnUndo.Enabled = this._undoredo.UseUndo;
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            (this._undoredo as UndoRedoManagerLimitHistory<Memento>).SetUndoMemento();
            this.SetBtnEnable();
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            this._undoredo.Undo();
            this.SetBtnEnable();
        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            this._undoredo.Redo();
            this.SetBtnEnable();
        }
    }
    public class Memento
    {
        public Memento(string val)
        {
            this.Value = val;
        }
        public string Value { get;private set; }
    }
}
