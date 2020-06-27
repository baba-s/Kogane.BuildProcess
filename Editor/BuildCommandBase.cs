namespace Kogane
{
	/// <summary>
	/// ビルドコマンドの抽象クラス
	/// </summary>
	public abstract class BuildCommandBase : IBuildCommand
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		/// <summary>
		/// タグを返します
		/// </summary>
		public abstract string Tag { get; }

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		public BuildCommandResult Run()
		{
			return DoRun();
		}

		/// <summary>
		/// 派生クラスで実行時の処理を記述します
		/// </summary>
		protected abstract BuildCommandResult DoRun();

		/// <summary>
		/// 後始末する時に呼び出されます
		/// </summary>
		public void CleanUp()
		{
			DoCleanUp();
		}

		/// <summary>
		/// 派生クラスで後始末の処理を記述します
		/// </summary>
		protected virtual void DoCleanUp()
		{
		}

		/// <summary>
		/// 実行結果が成功だった時に派生クラスから呼び出します
		/// </summary>
		protected BuildCommandResult Success()
		{
			return Success( string.Empty );
		}

		/// <summary>
		/// 実行結果が警告だった時に派生クラスから呼び出します
		/// </summary>
		protected BuildCommandResult Warning()
		{
			return Warning( string.Empty );
		}

		/// <summary>
		/// 実行結果がエラーだった時に派生クラスから呼び出します
		/// </summary>
		protected BuildCommandResult Error()
		{
			return Error( string.Empty );
		}

		/// <summary>
		/// <para>実行結果が成功だった時に派生クラスから呼び出します</para>
		/// <para>実行結果にメッセージを付与することができます</para>
		/// </summary>
		protected BuildCommandResult Success( string message )
		{
			return new BuildCommandResult( Tag, BuildCommandResultType.SUCCESS, message );
		}

		/// <summary>
		/// <para>実行結果が警告だった時に派生クラスから呼び出します</para>
		/// <para>実行結果にメッセージを付与することができます</para>
		/// </summary>
		protected BuildCommandResult Warning( string message )
		{
			return new BuildCommandResult( Tag, BuildCommandResultType.WARNING, message );
		}

		/// <summary>
		/// <para>実行結果がエラーだった時に派生クラスから呼び出します</para>
		/// <para>実行結果にメッセージを付与することができます</para>
		/// </summary>
		protected BuildCommandResult Error( string message )
		{
			return new BuildCommandResult( Tag, BuildCommandResultType.ERROR, message );
		}
	}
}