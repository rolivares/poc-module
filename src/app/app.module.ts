import { CSP_NONCE, NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

console.log('outside', document.querySelector('meta[name="csp-nonce"]')?.getAttribute('content'))

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    {
      provide: CSP_NONCE, useFactory: () => {
        const nonce = document.querySelector('meta[name="csp-nonce"]')?.getAttribute('content')
        console.log("nonce", nonce)
        return nonce
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
