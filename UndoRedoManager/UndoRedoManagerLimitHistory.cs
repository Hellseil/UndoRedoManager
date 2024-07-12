using System.Collections.Generic;

namespace UndoRedoManager
{
	public  class UndoRedoManagerLimitHistory<TMemento>:IUndoRedoble
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originator">発信者</param>
        public UndoRedoManagerLimitHistory(IMementoOriginator<TMemento> originator, int historyMax)
        {
            this._originator = originator;
            this._undoRedoList = new List<TMemento>();
            this._nowMemento = this._originator.CreateMemento();
            this._undoRedoList .Add(this._nowMemento);
            this._nowIndex = 0;
			this._historyMax = historyMax;
        }
        protected bool _isMementoSettingProcessing;
        protected bool _isAddable = true;
        protected int _historyMax;
        protected int _nowIndex;
        protected  IMementoOriginator<TMemento> _originator;
        protected  TMemento _nowMemento;
        protected List<TMemento> _undoRedoList;
        /// <summary>
        /// Undo可能か
        /// </summary>
        public bool UseUndo
        {
            get
            {
                return this._nowIndex >0;
            }
        }
        /// <summary>
        /// Redo可能か
        /// </summary>
        public bool UseRedo
        {
            get
            {
                return this._nowIndex != this._undoRedoList .Count-1  ;
            }
        }

        public void SetUndoMemento()
        {
            if (!this._isMementoSettingProcessing && this._isAddable)
            {
                if (this._nowIndex != -1)
                {
                    while (this._nowIndex != this._undoRedoList.Count - 1)
                    {
						this._undoRedoList.RemoveAt(this._undoRedoList.Count - 1);
                    }
                }
                this._nowIndex++;
                this._nowMemento = this._originator.CreateMemento();
				this._undoRedoList.Add(this._nowMemento);

                if (this._historyMax > 0)
                {
                    if (this._nowIndex >= this._historyMax)
                    {
                        this._nowIndex--;
						this._undoRedoList.RemoveAt(0);
                    }
                }
            }
        }
        public void Undo()
        {
			this._isMementoSettingProcessing = true;

			this._nowIndex--;
            this._nowMemento = this._undoRedoList[this._nowIndex];
            this._originator.SetMemento (this._nowMemento);

			this._isMementoSettingProcessing = false;
        }
        public void Redo()
        {
			this._isMementoSettingProcessing = true;

			this._nowIndex++;
			this._nowMemento = this._undoRedoList[this._nowIndex];
			this._originator.SetMemento(this._nowMemento);

			this._isMementoSettingProcessing = false;
        }
        public void ResetUndoRedo()
        {
            this._undoRedoList.Clear();
            this._undoRedoList.Add(this._nowMemento);
			this._nowIndex = 0;
        }
    }
}
