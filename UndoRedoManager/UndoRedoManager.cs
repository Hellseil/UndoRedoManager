using System.Collections.Generic;

namespace UndoRedoManager
{
	/// <summary>
	/// UndoRedoをサポートします
	/// </summary>
	/// <typeparam name="TMemento">記録クラス</typeparam>
	public  class UndoRedoManager<TMemento>:IUndoRedoble
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originator">発信者</param>
        public UndoRedoManager(IMementoOriginator<TMemento > originator ) 
        {
            this._originator = originator;
            this._isMementoSettingProcessing = false;
            this._UndoStack =new Stack<TMemento>();
            this._RedoStack =new Stack<TMemento>();
			this._nowMemento = this._originator.CreateMemento();
        }
        protected  IMementoOriginator<TMemento> _originator;
        protected  TMemento _nowMemento;
        protected bool _isMementoSettingProcessing;
        protected bool _isAddable = true;
        protected Stack<TMemento> _UndoStack;
        protected Stack<TMemento> _RedoStack;
        /// <summary>
        /// Undo可能か
        /// </summary>
        public virtual  bool UseUndo
        {
            get
            {
               return this._UndoStack.Count !=0;
            }
        }
        /// <summary>
        /// Redo可能か
        /// </summary>
        public virtual bool UseRedo
        {
            get
            {
                return this._RedoStack.Count != 0;
            }
        }

        public virtual void SetUndoMemento()
        {
            if (!this._isMementoSettingProcessing&& this._isAddable)
            {
				this._UndoStack.Push(this._nowMemento);
				this._nowMemento = this._originator.CreateMemento();
				this._RedoStack.Clear();
            }
        }
        public virtual void Undo()
        {
			this._isMementoSettingProcessing = true;

            this._RedoStack.Push(this._nowMemento );
            this._nowMemento = this._UndoStack.Pop();
			this._originator.SetMemento (this._nowMemento);

			this._isMementoSettingProcessing = false;
            
        }
        public virtual void Redo()
        {
			this._isMementoSettingProcessing = true;

			this._UndoStack.Push(this._nowMemento );
            this._nowMemento = this._RedoStack.Pop();
			this._originator .SetMemento (this._nowMemento );

			this._isMementoSettingProcessing = false;
        }
        public virtual void ResetUndoRedo()
        {
			this._UndoStack.Clear();
			this._RedoStack.Clear();
        }

    }
}
