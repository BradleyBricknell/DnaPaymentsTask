# DnaPaymentsTask


Welcome to my implementation of the task to create the beginings of a banking account system.

My banking system uses the criteria as a jumping off point and explores the ideas on how  the system can improved on after the initial criteria is met.

The provided criteria
-   Support for different account types with the possibility for future expansion. For now, Current and Savings accounts are the only requirements. ✓
-   Ability to create and retrieve accounts. ✓
-   Ability to freeze(disable) an account. ✓
-   Written in C# (.NET 6 and onwards). ✓

I thought it was appropriate to make the initial version of this software to take into account multiple potential extenstion tasks, and all for scalability in its features. Without having to rewrite large chunks of the application.
 
 I have drawn up the following database design. From which I have used as a base when creating my Entity classes for the in-memory database solution
 ![initial database design](https://i.imgur.com/5dSPPi5.png)
___
### Feature to note
As per one of the criteria, we want to manage different account types. As well as allowing additional accounttypes to be created in the future.

I propose by tracking the different AccountTypes in its own `accountTypes` table in our database. We can simply track what type of account it is. The database has been design in a way where we can add additional fields to the accountType  table to differenciate behaviour between accountTypes.


####  `accountType` table:

 `fullfillNextDayTransfer` which we can set to communicate to the front-end that monies moving between accounts have to wait until the next business day. This will be set to true for the savings account.
 
 `customerToBusinessPaymentsAllowed` this can be set so only certain account type can accept payments from other current accounts/customer accounts. Such as a current account. A hypothetical use case for this would be that the savings accounts can only transfer monies between the user's accounts, and deline any payments from external accounts.

#### `userMembershiptype` table
In the spirit of thinking about future functionality. I have taken the liberty of implementing a `userMembershipType` entity in the database. This will allow us to track different membership types within our banking system. With fields to allow us to configure behaviour based on a user's membership Level

In the code you will find 3 types of membership `Free`, `Plus`, and `Premium`. With each type allowing users to have increased benefits (such as a higher limit on the number of accounts a user may create), as well as increased interest rates for the higher tiers of members. _these extended use cases are hypothetical in nature, while the database design proposes this type of feature. The code base does not have this implemented_.


___
I look forward to meeting with you in the near future where I will be very happy to discuss my approach and thought process behind this implemenation.

Kind regards
