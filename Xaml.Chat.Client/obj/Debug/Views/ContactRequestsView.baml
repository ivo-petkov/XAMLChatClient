   M S B A M L   `   `   `  ����  Xaml.Chat.Client0    *Xaml.Chat.Client.Views.ContactRequestsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �� .+�5   
   �� -   5          ��Name$  listBoxContactRequests��5      ��62   �� *ContactRequests+��5	      �� 5
      .R�5      � .+�5      �� $��30��6L   $��30��6W   ��6    �� *Sender.ProfilePictureUrl+6W   �� 5       �� Padding$ 3q�6$   $��13(�5   (   ��5   0   �� *Sender.Username+5   (   5      5      5      � 5   
   .+�5      �� ��6   �� *AcceptRequest+��5      ��  �� ElementName$ listBoxContactRequests�� �� Path$ SelectedValue�.��6m   Accept6u   �� 5      ��6   �� *DenyRequest+��5      �� $ listBoxContactRequests��$ SelectedValue�.��6m   Deny6s   5      �� 5   
   $��Center=�6   
��� � �$
��Bold"�6D   ��5      �� *SuccessMessage+�� 5   
   $��Center=�6   
���  ��$
��Bold"�6B   ��5      �� *ErrorMessage+5      5      