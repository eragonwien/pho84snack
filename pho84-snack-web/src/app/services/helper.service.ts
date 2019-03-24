import { Injectable, ElementRef } from "@angular/core";

@Injectable()
export class HelperService {
  DEFAULT_IMAGE: string = "default";
  IMAGE_SQUARE = "512x512";
  IMAGE_16_9 = "1600x900";
  IMAGE_3_1 = "1800x600";

  constructor() {}

  image(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + ".jpg";
  }

  imageSquare(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + "-" + this.IMAGE_SQUARE + ".jpg";
  }

  image16By9(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + "-" + this.IMAGE_16_9 + ".jpg";
  }

  image3By1(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/" + image + "-" + this.IMAGE_3_1 + ".jpg";
  }

  imageUrlPath(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "url('" + image + "')";
  }

  getPath(url: string): string {
    url = url.split(",").join("/");
    if (!url.startsWith("/")) {
      url = "/" + url;
    }
    return url;
  }

  SHOW_STATE: string = "show";
  HIDE_STATE: string = "hide";
  DEFAULT_OFFSET: number = 500;
  getScrollState(state: string, element: ElementRef): string {
    if (state === this.SHOW_STATE) {
      return state;
    }
    const componentPosition = element.nativeElement.offsetTop - this.DEFAULT_OFFSET;
    const scrollPosition = window.pageYOffset;
    if (scrollPosition >= componentPosition) {
      state = this.SHOW_STATE;
    } else {
      state = this.HIDE_STATE;
    }

    return state;
  }
}
