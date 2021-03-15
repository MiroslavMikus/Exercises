## Description

Example project how to use material design.

### How to

1. Open Console and enter: 

> ng new Angular.Material
> cd .\Angular.Material\
> npm i --save @angular/cdk @angular/material @angular/animations

2. Open styles.css and add: 

> @import "~@angular\\material\\prebuilt-themes\\indigo-pink.css" 

3. Open app.modules.ts and add BrowserAnimationsModule

> import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

and also to __@NgModule.imports__.
