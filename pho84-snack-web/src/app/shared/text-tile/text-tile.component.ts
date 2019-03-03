import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-text-tile",
  templateUrl: "./text-tile.component.html",
  styleUrls: ["./text-tile.component.css"]
})
export class TextTileComponent implements OnInit {
  @Input() title: string;
  @Input() subtitle: string;
  @Input() text: string;
  @Input() buttonText: string;
  @Input() buttonUrl: string;
  @Input() isHiddenMobile: boolean;
  constructor() {}

  ngOnInit() {}
}
