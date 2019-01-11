using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WindBot.Game.AI
{
	[DataContract]
	public class DialogsData
	{
		[DataMember]
		public string[] welcome { get; set; }
		[DataMember]
		public string[] deckerror { get; set; }
		[DataMember]
		public string[] duelstart { get; set; }
		[DataMember]
		public string[] newturn { get; set; }
		[DataMember]
		public string[] endturn { get; set; }
		[DataMember]
		public string[] directattack { get; set; }
		[DataMember]
		public string[] attack { get; set; }
		[DataMember]
		public string[] ondirectattack { get; set; }
		[DataMember]
		public string facedownmonstername { get; set; }
		[DataMember]
		public string[] activate { get; set; }
		[DataMember]
		public string[] summon { get; set; }
		[DataMember]
		public string[] setmonster { get; set; }
		[DataMember]
		public string[] chaining { get; set; }   
		[DataMember]
		public string[] choiceadd { get; set; }
		[DataMember]
		public string[] choiceselect { get; set; }
		[DataMember]
		public string[] counter { get; set; }
		[DataMember]
		public string[] rps { get; set; }
		[DataMember]
		public string[] rpswin { get; set; }
		[DataMember]
		public string[] tribute { get; set; }
		[DataMember]
		public string[] bosssummon { get; set; }
	}
	public class Dialogs
	{
		private GameClient _game;

		private string[] _welcome;
		private string[] _deckerror;
		private string[] _duelstart;
		private string[] _newturn;
		private string[] _endturn;
		private string[] _directattack;
		private string[] _attack;
		private string[] _ondirectattack;
		private string _facedownmonstername;
		private string[] _activate;
		private string[] _summon;
		private string[] _setmonster;
		private string[] _chaining;
		private string[] _choiceadd;
		private string[] _choiceselect;
		private string[] _counter;
		private string[] _rps;
		private string[] _rpswin;
		private string[] _tribute;
		private string[] _bosssummon;
		public Dialogs(GameClient game)
		{
			_game = game;
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DialogsData));
			string dialogfilename = game.Dialog;
			using (FileStream fs = File.OpenRead("Dialogs/" + dialogfilename + ".json"))
			{
				DialogsData data = (DialogsData)serializer.ReadObject(fs);
				_welcome = data.welcome;
				_deckerror = data.deckerror;
				_duelstart = data.duelstart;
				_newturn = data.newturn;
				_endturn = data.endturn;
				_directattack = data.directattack;
				_attack = data.attack;
				_ondirectattack = data.ondirectattack;
				_facedownmonstername = data.facedownmonstername;
				_activate = data.activate;
				_summon = data.summon;
				_setmonster = data.setmonster;
				_chaining = data.chaining;
				_choiceadd = data.choiceadd;
				_choiceselect = data.choiceselect;
				_counter = data.counter;
				_rps = data.rps;
				_rpswin = data.rpswin;
				_tribute = data.tribute;
				_bosssummon = data.bosssummon;
			}
		}

		public void SendSorry()
		{
			InternalSendMessageForced(new[] { "Sorry, an error occurs." });
		}

		public void SendDeckSorry(string card)
		{
			if (card == "DECK")
				InternalSendMessageForced(new[] { "Deck illegal. Please check the database of your YGOPro and WindBot." });
			else
				InternalSendMessageForced(_deckerror, card);
		}

		public void SendWelcome()
		{
			InternalSendMessage(_welcome);
		}

		public void SendDuelStart()
		{
			InternalSendMessage(_duelstart);
		}

		public void SendNewTurn()
		{
			InternalSendMessage(_newturn);
		}

		public void SendEndTurn()
		{
			InternalSendMessage(_endturn);
		}

		public void SendDirectAttack(string attacker)
		{
			InternalSendMessage(_directattack, attacker);
		}

		public void SendAttack(string attacker, string defender)
		{
			if (defender=="monster")
			{
				defender = _facedownmonstername;
			}
			InternalSendMessage(_attack, attacker, defender);
		}

		public void SendOnDirectAttack(string attacker)
		{
			if (string.IsNullOrEmpty(attacker))
			{
				attacker = _facedownmonstername;
			}
			InternalSendMessage(_ondirectattack, attacker);
		}
		public void SendOnDirectAttack()
		{
			InternalSendMessage(_ondirectattack);
		}

		public void SendActivate(string spell)
		{
			InternalSendMessage(_activate, spell);
		}

		public void SendSummon(string monster)
		{
			InternalSendMessage(_summon, monster);
		}

		public void SendSetMonster()
		{
			InternalSendMessage(_setmonster);
		}

		public void SendChaining(string card)
		{
			InternalSendMessage(_chaining, card);
		}
		public void SendChoiceAdd()
		{
			InternalSendMessage(_choiceadd);
		}
		public void SendChoiceSelect()
		{
			InternalSendMessage(_choiceselect);
		}
		public void sendcounter()
		{
			InternalSendMessage(_counter);
		}
		public void SendRps()
		{
			InternalSendMessage(_rps);
		}
		public void sendRpsWin()
		{
			InternalSendMessage(_rpswin);
		}
		public void SendTribute()
		{
			InternalSendMessage(_tribute);
		}
		public void SendBossSummon(string card)
		{
			InternalSendMessage(_bosssummon, card);
		}

		private void InternalSendMessage(IList<string> array, params object[] opts)
		{
			if (!_game._chat)
				return;
			try
			{
				string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
				if (message != "")
					_game.Chat(message);
			}
			catch { }

		}

		private void InternalSendMessageForced(IList<string> array, params object[] opts)
		{
			string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
			if (message != "")
			{
				_game.Chat(message);
				Logger.WriteLine("Error: " + message);
			}
		}
	}
}
