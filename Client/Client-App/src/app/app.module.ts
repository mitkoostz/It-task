import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventTableComponent } from './EventOdd/event-table/event-table.component';
import { EventModule } from './EventOdd/event.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    EventModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
