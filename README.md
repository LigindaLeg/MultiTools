![Total downloads](https://img.shields.io/github/downloads/LigindaLeg/MultiTools/total)
# MultiTools
MultiTools plugin for Exiled has many features


Default Config:
``` multi_tools:
  # Is the plugin enabled?
  is_enabled: true
  # Are debug messages displayed?
  debug: false
  # How much time does a cheater have before being banned?
  cheat_time: 30
  # Cheat Ban Reason?
  ban_reason: 'You are banned for cheating [MultiTools]'
  # Cuff Command?
  cuff_enabled: true
  # Cuff Range?
  cuff_range: 3
  # Cuff Process Delay?
  cuff_del: 1
  # Tesla Ignore Roles?
  ignore_tesla_roles:
  - NtfCaptain
  - NtfPrivate
  - NtfSergeant
  - NtfSpecialist
  - FacilityGuard
  # Discord Webhook to Ban-Notify?
  webhook_notify_ban: 'Paste your webhook here'
  # Discord Message Template?
  d_s_message: '{bantime} \n```html\n<Выдал:> {admin} \n<Нарушитель:> {bad} \n<Причина:> {reason} \n
  # Update interval for the hint in seconds. // interwal aktualizacji hint.
  update_interval: 1 
  # Server name to be displayed in the hint.
  server_name: 'YOU SERVER NAME' // Nazwa serwera.
  # Round time format. Use placeholders: {0} - minutes, {1} - seconds.
  round_time_format: ' {0}:{1}' // NIE DOTYKAC!
  # Hint text format. Use placeholders: {0} - player role color, {1} - player nickname, {2} - player role, {3} - spectator count, {4} - kill count, {5} - server name, {6} - round time.
  hint_text: |2-
    <voffset=-0.2em><pos=-300><size=15><align=left><voffset=0><color={0}>👤</color> <color={0}>| You name:</color> {1}
    <voffset=-0.2em><pos=-300><color={0}>🎭</color> <color={0}>| You play for:</color> {2}
    <voffset=-0.2em><pos=-300><color={0}>👥</color> <color={0}>| You're being watched:</color> <color={0}>{3}</color></voffset></pos>
    <size=20><align=center><voffset=-32em><u><pos=-0>{5}</pos></u></voffset>\n<pos=-0></pos></align></size>```'
 ```


Plugin permissions:
```
mt.cheater
mt.reverse 
mt.blockdoors
mt.check
mt.warn
 ```


Plugin Commands:
```
cheater (id) - Forces player to cheat checking
reverse [id] - Reverse player
blockdoors (id) - Set enabled or disabled to lock/unlock doors on interacting
.cuff - Cuffing the teammates
.call - Calls all admins
warn <add, delete, list> (id) [reason] - Manage Player Warnings
playercheck - Check player violations
```

Supported Exiled 8.9.6+
