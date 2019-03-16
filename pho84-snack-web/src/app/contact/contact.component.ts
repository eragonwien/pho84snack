import { Component, OnInit } from "@angular/core";
import { fadeInOut } from "../shared/animations";

@Component({
  selector: "app-contact",
  templateUrl: "./contact.component.html",
  styleUrls: ["./contact.component.css"],
  animations: [fadeInOut]
})
export class ContactComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
