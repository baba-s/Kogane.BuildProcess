//using JetBrains.Annotations;
//using System;
//using UnityEditor.TestTools.TestRunner.Api;
//using UnityEngine;

//namespace Kogane
//{
//	/// <summary>
//	/// Test Runner を実行するコマンドを管理するクラス
//	/// </summary>
//	public sealed class ExecuteTestRunnerCommand : BuildCommandBase
//	{
//		//==============================================================================
//		// プロパティ
//		//==============================================================================
//		public override string Tag => nameof( ExecuteTestRunnerCommand );

//		//==============================================================================
//		// 変数(readonly)
//		//==============================================================================
//		private readonly ExecutionSettings m_executionSettings;

//		//==============================================================================
//		// 関数
//		//==============================================================================
//		/// <summary>
//		/// コンストラクタ
//		/// </summary>
//		public ExecuteTestRunnerCommand( ExecutionSettings executionSettings )
//		{
//			m_executionSettings = executionSettings;
//		}

//		/// <summary>
//		/// コンストラクタ
//		/// </summary>
//		public ExecuteTestRunnerCommand( Filter filter ) : this( new ExecutionSettings( filter ) )
//		{
//		}

//		/// <summary>
//		/// 実行する時に呼び出されます
//		/// </summary>
//		protected override BuildCommandResult DoRun()
//		{
//			ITestResultAdaptor result = null;

//			var testRunnerApi = ScriptableObject.CreateInstance<TestRunnerApi>();
//			var callbacks     = new Callbacks( x => result = x );

//			testRunnerApi.RegisterCallbacks( callbacks );
//			testRunnerApi.Execute( m_executionSettings );

//			var json    = new JsonITestResultAdaptor( result );
//			var message = json.ToString();

//			return result.TestStatus == TestStatus.Passed
//					? Success( message )
//					: Error( message )
//				;
//		}

//		//==============================================================================
//		// クラス
//		//==============================================================================
//		private sealed class Callbacks : ICallbacks
//		{
//			private readonly Action<ITestResultAdaptor> m_onFinished;

//			public Callbacks( Action<ITestResultAdaptor> onFinished )
//			{
//				m_onFinished = onFinished;
//			}

//			public void RunStarted( ITestAdaptor testsToRun )
//			{
//			}

//			public void RunFinished( ITestResultAdaptor result )
//			{
//				m_onFinished( result );
//			}

//			public void TestStarted( ITestAdaptor test )
//			{
//			}

//			public void TestFinished( ITestResultAdaptor result )
//			{
//			}
//		}

//		//==============================================================================
//		// 構造体
//		//==============================================================================
//		[Serializable]
//		private struct JsonITestResultAdaptor
//		{
//			[SerializeField][UsedImplicitly] private string       Message;
//			[SerializeField][UsedImplicitly] private bool         HasChildren;
//			[SerializeField][UsedImplicitly] private int          InconclusiveCount;
//			[SerializeField][UsedImplicitly] private int          SkipCount;
//			[SerializeField][UsedImplicitly] private int          PassCount;
//			[SerializeField][UsedImplicitly] private int          FailCount;
//			[SerializeField][UsedImplicitly] private int          AssertCount;
//			[SerializeField][UsedImplicitly] private string       StackTrace;
//			[SerializeField][UsedImplicitly] private string       Output;
//			[SerializeField][UsedImplicitly] private string       StartTime;
//			[SerializeField][UsedImplicitly] private double       Duration;
//			[SerializeField][UsedImplicitly] private TestStatus   TestStatus;
//			[SerializeField][UsedImplicitly] private string       ResultState;
//			[SerializeField][UsedImplicitly] private string       FullName;
//			[SerializeField][UsedImplicitly] private string       Name;
//			[SerializeField][UsedImplicitly] private ITestAdaptor Test;
//			[SerializeField][UsedImplicitly] private string       EndTime;

//			public JsonITestResultAdaptor( ITestResultAdaptor other )
//			{
//				var jstZoneInfo = TimeZoneInfo.Local;

//				Message           = other.Message;
//				HasChildren       = other.HasChildren;
//				InconclusiveCount = other.InconclusiveCount;
//				SkipCount         = other.SkipCount;
//				PassCount         = other.PassCount;
//				FailCount         = other.FailCount;
//				AssertCount       = other.AssertCount;
//				StackTrace        = other.StackTrace;
//				Output            = other.Output;
//				StartTime         = TimeZoneInfo.ConvertTimeFromUtc( other.StartTime, jstZoneInfo ).ToString( "yyyy/MM/dd HH:mm:ss" );
//				Duration          = other.Duration;
//				TestStatus        = other.TestStatus;
//				ResultState       = other.ResultState;
//				FullName          = other.FullName;
//				Name              = other.Name;
//				Test              = other.Test;
//				EndTime           = TimeZoneInfo.ConvertTimeFromUtc( other.EndTime, jstZoneInfo ).ToString( "yyyy/MM/dd HH:mm:ss" );
//			}

//			public override string ToString()
//			{
//				return JsonUtility.ToJson( this, true );
//			}
//		}
//	}
//}