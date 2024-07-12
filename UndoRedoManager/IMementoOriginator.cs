namespace UndoRedoManager
{
	/// <summary>
	/// 発信者であること提供します
	/// </summary>
	/// <typeparam name="TMemento">メメントクラス</typeparam>
	public  interface  IMementoOriginator<TMemento>
    {
        /// <summary>
        /// メメント(記録)を取得します。
        /// </summary>
        /// <remarks>参照型の場合必ずディープコピーを行ってください</remarks>
        /// <returns>記録</returns>
        TMemento CreateMemento();
        /// <summary>
        /// メメントを現在の値にセットします。
        /// </summary>
        /// <param name="memento"></param>
        void SetMemento(TMemento memento);

    }
}
