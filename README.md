![Total downloads](https://img.shields.io/github/downloads/LigindaLeg/MultiTools/total)
# MultiTools
MultiTools plugin for Exiled has many features


Default Config:
```
multi_tools:
  # Is the plugin enabled?
  is_enabled: true
  # Are debug messages displayed?
  debug: false
  # How much time does a cheater have before being banned?
  cheat_time: 30
  # Cheat Ban Reason?
  ban_reason: 'You are banned for cheating [MultiTools]'
  # Discord Webhook to Ban-Notify?
  webhook_notify_ban: 'Paste your webhook here'
  # Discord Message Template?
  d_s_message: '{bantime} \n```html\n<Выдал:> {admin} \n<Нарушитель:> {bad} \n<Причина:> {reason} \n
 ```


Plugin permissions:
```
mt.cheater
mt.reverse 
mt.blockdoors
mt.check
mt.warn
mt.customprefix
mt.permanentrole
 ```


Plugin Commands:
```
cheater (id) - Forces player to cheat checking
reverse [id] - Reverse player
blockdoors (id) - Set enabled or disabled to lock/unlock doors on interacting
.call - Calls all admins
warn <add, delete, list> (id) [reason] - Manage Player Warnings
playercheck (id) - Check player violations
customprefix (id) (color) (prefix) - Set custom prefix to player
pr (ID) (role) - Set permanent player group on server
```

Supported Exiled 8.9.6+
