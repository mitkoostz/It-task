import { formatDate } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { cwd } from 'process';
import { IEvent } from 'src/Models/Event';
import { EventService } from '../event.service';

@Component({
  selector: 'app-event-table',
  templateUrl: './event-table.component.html',
  styleUrls: ['./event-table.component.scss']
})
export class EventTableComponent implements OnInit {
  @ViewChild('errorLabel') errorLabel;
  @ViewChild('successLabel') successLabel;
  editEnabled: boolean = false;
  events: IEvent[] = [];
  eventUpdateForm: FormGroup;
  currentDate: Date;

  constructor(
    private eventService: EventService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadAllEvents();
    this.createEventForms();
    this.currentDate = new Date();
  }

  fieldStartEdit(event) {
    event.srcElement.disabled = false;
    event.srcElement.focus();

  }
  fieldEditLooseFocus(event) {
    event.srcElement.disabled = true;
  }

  loadAllEvents() {
    this.eventService.loadAllEvents().subscribe(response => {
      this.events = response;
      this.createEventForms();
    }, error => {
      console.log(error);
    });
  }

  addNewEvent() {
    this.eventService.addNewEvent().subscribe(response => {
      this.events.push(response);
      this.createEventForms();
      this.formArr.push(this.initRows(response.id, response.eventName,
        response.oddsForFirstTeam,
        response.oddsForDraw,
        response.oddsForSecondTeam,
        response.eventStartDate));
    }, error => {
      console.log(error);
    });
  }

  updateEvent(id: number, formId: number) {
    var event: IEvent = this.events.find(e => e.id === id);
    if (event === undefined) {
      console.log("Product you are updating doesn't exist!")
      return;
    }
    let eventFormData = this.formArr.get(formId.toString()).value;
    //eventFormData.eventStartDate =  this.convertDateToJson(eventFormData.eventStartDate);
    if (!this.validateFields(eventFormData.oddsForFirstTeam,
      eventFormData.oddsForDraw, eventFormData.oddsForSecondTeam,
      eventFormData.eventStartDate.toString())) {
      this.errorLabel.nativeElement.hidden = false;
      this.errorLabel.nativeElement.innerHTML = "The odds must be equal or greater than 1. And the date must be in correct format!";
      return;
    } else {
      this.errorLabel.nativeElement.hidden = true;
      this.errorLabel.nativeElement.innerHTML = "";
    }
    this.eventService.updateEvent(eventFormData).subscribe(response => {
      this.loadAllEvents();
      this.createEventForms();
      this.successLabel.nativeElement.hidden = false;
      this.successLabel.nativeElement.innerHTML = "Successfully saved event.";
    }, error => {
      console.log(error);
    });
  }

  deleteEvent(id: number) {
    this.eventService.deleteEvent(id).subscribe(response => {
      this.events = this.events.filter(e => e.id != id);
    }, error => {
      console.log(error);
    });
  }

  createEventForms() {
    this.eventUpdateForm = this.fb.group({
      Rows: this.fb.array([])
    });
    let index = 0;
    this.events.forEach(event => {
      this.formArr.push(this.initRows(event.id, event.eventName,
        event.oddsForFirstTeam, event.oddsForDraw,
        event.oddsForSecondTeam, event.eventStartDate));
      event.formId = index;
      index++;
    });
  }

  get formArr() {
    return this.eventUpdateForm.get("Rows") as FormArray;
  }

  initRows(id: number,
    eventName: string,
    oddsForFirstTeam: number,
    oddsForDraw: number,
    oddsForSecondTeam: number,
    eventStartDate: Date) {

    return this.fb.group({
      id: [id],
      eventName: [eventName],
      oddsForFirstTeam: [oddsForFirstTeam.toFixed(2)],
      oddsForDraw: [oddsForDraw.toFixed(2)],
      oddsForSecondTeam: [oddsForSecondTeam.toFixed(2)],
      eventStartDate: [eventStartDate] //[formatDate(eventStartDate, 'dd/MM/yyyy h:mm', 'en')]
    });

  }
  //java script date bug when using dd/MM/yyyy h:mm date
  private convertDateToJson(date: string) {
    if (!isNaN(Date.parse(date))) {
      return date;
    }
    let dmyyyytime = date.split('/');
    if (dmyyyytime.length === 0) return;
    let yyytime = dmyyyytime[2].split(' ');
    if (yyytime.length === 0) return;
    let minsec = yyytime[1].split(':');

    var year: number = +yyytime[0];
    var mounth: number = +dmyyyytime[1];
    var day: number = +dmyyyytime[0];
    var hours: number = +minsec[0];
    var min: number = +minsec[1];

    return new Date(year, mounth, day, hours, min);
  }

  private validateFields(first: number, draw: number, second: number, date: string): boolean {
    let tryDate = Date.parse(date);
    if (isNaN(tryDate)) {
      return false;
    }
    if (first >= 1 && draw >= 1 && second >= 1) {
      return true;
    }
    return false;
  }

  checkIfDateIsPassed(eventDate: string): boolean {

    if (new Date(eventDate.toString()).getTime() < new Date().getTime()) {
      return true;
    }
    return false;
  }

  writeDataToConsole(event: IEvent, oddsFor: number) {
    let typeOfOdd: string;
    let valueOfOdd: number;
    switch (oddsFor) {
      case 1:
        typeOfOdd = "OddsForFirstTeam";
        valueOfOdd = event.oddsForFirstTeam;
        break;
      case 2:
        typeOfOdd = "OddsForDraw";
        valueOfOdd = event.oddsForDraw;
        break;
      case 3:
        typeOfOdd = "OddsForSecondTeam";
        valueOfOdd = event.oddsForSecondTeam;
        break;
      default:
        break;
    }
    console.log(`${event.id} ${typeOfOdd} ${valueOfOdd.toFixed(2)}`);
  }
}
