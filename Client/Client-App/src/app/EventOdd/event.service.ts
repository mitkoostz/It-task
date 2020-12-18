import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEvent } from 'src/Models/Event';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  loadAllEvents() {
    return this.http.get<IEvent[]>(this.baseUrl + "EventOdds/getevents").pipe(
      map(response => {
        return response;
      }));
  }

  addNewEvent() {
    return this.http.post<IEvent>(this.baseUrl + "EventOdds/addnewevent", null).pipe(
      map(response => {
        return response;
      }));
  }
  updateEvent(event: IEvent) {
    return this.http.post<IEvent>(this.baseUrl + "EventOdds/updateevent", event).pipe(
      map(response => {
        return response;
      }));
  }
  deleteEvent(id: number) {  
    let params = new HttpParams();
    params = params.append('id', id.toString());

    return this.http.delete(this.baseUrl + "EventOdds/deleteevent", {params}).pipe(
      map(response => {
        return response;
      }));
  }

}
