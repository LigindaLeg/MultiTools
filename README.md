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
  d_s_message: '{bantime} \n```html\n<Выдал:> {admin} \n<Нарушитель:> {bad} \n<Причина:> {reason} \n```'
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
