import { Directive, OnInit, ElementRef, Renderer2, HostListener } from '@angular/core';

@Directive({
    selector: '[appCardDirective]'
})

export class CardDirective implements OnInit {
    constructor(
        private element: ElementRef,
        private renderer: Renderer2
      ) { }

    ngOnInit() { }

    @HostListener('mouseenter') onMouseEnter(enentData: Event) {
        this.renderer.setStyle(this.element.nativeElement, 'backgroundColor', 'red');
    }

    @HostListener('mouseleave') onMouseLeave(enentData: Event) {
        this.renderer.setStyle(this.element.nativeElement, 'backgroundColor', 'green');
    }
}
