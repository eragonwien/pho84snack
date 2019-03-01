import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-hcard',
  templateUrl: './hcard.component.html',
  styleUrls: ['./hcard.component.css']
})
export class HcardComponent implements OnInit {

  @Input() title: string;
  @Input() subtitle: string;
  @Input() text: string;
  @Input() buttonText: string;
  @Input() buttonUrl: string;
  @Input() image: string;
  @Input() align: string;

  constructor() { }

  ngOnInit() {
    this.image = "/assets/images/compressed/" + this.image + ".jpg";
  }

  get backgroundImage(): string { return "url('" + this.image + "')"}
}
