import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-news",
  templateUrl: "./news.component.html",
  styleUrls: ["./news.component.css"]
})
export class NewsComponent implements OnInit {
  @Input() title: string;
  @Input() subtitle: string;
  @Input() text: string;
  @Input() image: string;

  constructor() {}

  ngOnInit() {}
}
