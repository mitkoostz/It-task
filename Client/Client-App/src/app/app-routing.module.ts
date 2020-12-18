import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EventTableComponent } from './EventOdd/event-table/event-table.component';

const routes: Routes = [
  {path: '', component: EventTableComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
